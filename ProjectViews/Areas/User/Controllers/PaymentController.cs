using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using ProjectViews.Areas.User.Models;
using ProjectViews.Models;
using System.Text;

namespace ProjectViews.Areas.User.Controllers
{

    [Area("User")]
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            ViewData["user"] = HttpContext.Session.GetString("User");

            // Lay Danh sach san pham
            string apiUrlshoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            var responseshoe = await _httpClient.GetAsync(apiUrlshoe); // goi api lay data
            string apiDatashoe = await responseshoe.Content.ReadAsStringAsync(); // doc data tra ve
            var shoeDetails = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDatashoe);

            // Lay danh sach toan bo size
            string apiURLSize = $"https://localhost:7109/api/Size/get-all-size";
            var responseSize = await _httpClient.GetAsync(apiURLSize);
            string apiDataSize = await responseSize.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Sizes>>(apiDataSize);

            // Lay danh sach toan bo size cua san pham
            string apiURLSizeShoe = $"https://localhost:7109/api/SIzes_ShoeDetails/get-all-size-shoe-details";
            var responseSizeShoe = await _httpClient.GetAsync(apiURLSizeShoe);
            string apiDataSizeShoe = await responseSizeShoe.Content.ReadAsStringAsync();
            var sizeShoes = JsonConvert.DeserializeObject<List<Sizes_ShoeDetails>>(apiDataSizeShoe);

            // Lay anh cua san pham
            string apiImage = $"https://localhost:7109/api/Images/get-all-image";
            var reponce3 = await _httpClient.GetAsync(apiImage);
            var apiDataImage = await reponce3.Content.ReadAsStringAsync();
            var images = JsonConvert.DeserializeObject<List<Images>>(apiDataImage);

            // Lay phuong thuc thanh toan
            string apiURLPayMethod = $"https://localhost:7109/api/PaymentMethod";
            var responsePayMethod = await _httpClient.GetAsync(apiURLPayMethod);
            var apiDataPayMethod = await responsePayMethod.Content.ReadAsStringAsync();
            var paymentMethods = JsonConvert.DeserializeObject<List<PaymentMethods>>(apiDataPayMethod);

            // Lay cac thong tin san co cua user
            string username = HttpContext.Session.GetString("User");
            string apiUrlUser = "https://localhost:7109/api/User/get-all-user";
            var responseUser = await _httpClient.GetAsync(apiUrlUser);
            string apidataUser = await responseUser.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(apidataUser);
            var User = users.FirstOrDefault(p => p.Username == username);

            // Lay danh sach CartDetail de thanh toan
            string apiURLCartDetail = $"https://localhost:7109/api/CartDetail";
            var responseCartDetail = await _httpClient.GetAsync(apiURLCartDetail);
            var apiDataCartDetail = await responseCartDetail.Content.ReadAsStringAsync();
            var cartdetails = JsonConvert.DeserializeObject<List<CartDetails>>(apiDataCartDetail);

            // Lay danh sach cac Coupon
            string apiURL = $"https://localhost:7109/api/Coupons/Show-Coupons";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<List<Coupons>>(apiData);

            // Lay danh sach cac ShipMethod
            string apiURLAddress = $"https://localhost:7109/api/ShipMethod";
            var responseADdress = await _httpClient.GetAsync(apiURLAddress);
            var apiDataAddress = await responseADdress.Content.ReadAsStringAsync();
            var shipmethods = JsonConvert.DeserializeObject<List<ShipAdressMethod>>(apiDataAddress);

       

            // Them cac item san pham vao list
            PaymentViewModel paymentVMD = new PaymentViewModel();


            List<CartViewModel> listCartItem = new List<CartViewModel>();
            double tamTinhVari = 0;
            double Shipping = 0;
            double GiamGiaVoucher = 0;
            double TongTienPhaiTra = 0;
            foreach (var item in cartdetails)
            {
                CartViewModel cartItemVMD = new CartViewModel();
                var shoeItem = shoeDetails.FirstOrDefault(p => p.Id == item.IdShoeDetail);
                cartItemVMD.IdCartDetail = item.Id;
                cartItemVMD.IdShoe = item.IdShoeDetail;
                cartItemVMD.ShoeName = shoeItem.Name;

                var sizeShoe = sizeShoes.FirstOrDefault(p => p.IdShoeDetails == item.IdShoeDetail);
                var size = sizes.FirstOrDefault(p => p.Id == sizeShoe.IdSize);
                cartItemVMD.sizeNumber = size.SizeNumber;

                cartItemVMD.price = shoeItem.SellPrice;
                cartItemVMD.quantity = item.Quantity;
                cartItemVMD.SumPriceOfOne = item.Quantity * shoeItem.SellPrice;

                var imageShoe = images.FirstOrDefault(p => p.IdShoeDetail == item.IdShoeDetail);
                if(imageShoe == null)
                {
                    cartItemVMD.ImageSource = "abc";
                }
                else
                {
                    cartItemVMD.ImageSource = imageShoe.ImageSource;
                }
              

                tamTinhVari += item.Quantity * shoeItem.SellPrice;

                listCartItem.Add(cartItemVMD);


            }

            paymentVMD.listCartItems = listCartItem;
            paymentVMD.paymentMethods = paymentMethods;
            paymentVMD.shipAdressMethods = shipmethods;
            paymentVMD.coupons = coupons;
            paymentVMD.User = User;
            TongTienPhaiTra = tamTinhVari + Shipping - GiamGiaVoucher * tamTinhVari;
            paymentVMD.TongTienPhaiTra = TongTienPhaiTra;

            return View(paymentVMD);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPay(Guid idShipMethod, Guid idPaymentMethod, string fullname, string sdt, string tinh, string huyen, string xa, string duong, string diachi, Guid idCoupon)
        {
            // Create Location of user --- Ok
            string apiURL = $"https://localhost:7109/api/Location/Createlocation?stage={tinh}&District={huyen}&ward={xa}&street={duong}&Address={diachi}";
            var response1 = await _httpClient.PostAsync(apiURL, null);

            // Get user after login
            string username = HttpContext.Session.GetString("User");
            string apiUrlUser = "https://localhost:7109/api/User/get-all-user";
            var responseUser = await _httpClient.GetAsync(apiUrlUser);
            string apidataUser = await responseUser.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(apidataUser);
            var User = users.FirstOrDefault(p => p.Username == username);

            // Get all cart item of user
            string apiURLCart = $"https://localhost:7109/api/CartDetail";
            var responseCart = await _httpClient.GetAsync(apiURLCart);
            var apiDataCart = await responseCart.Content.ReadAsStringAsync();
            var cartdetails = JsonConvert.DeserializeObject<List<CartDetails>>(apiDataCart);
            var cartDetailsByUser = cartdetails.Where(p => p.IdUser == User.Id).ToList();

            // Get all shoe
            string apiUrlshoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            var responseshoe = await _httpClient.GetAsync(apiUrlshoe); // goi api lay data
            string apiDatashoe = await responseshoe.Content.ReadAsStringAsync(); // doc data tra ve
            var shoeDetails = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDatashoe);

            // Get all location
            string apiUrlLocation = $"https://localhost:7109/api/Location";
            var responseLocation = await _httpClient.GetAsync(apiUrlLocation);
            string apidataLocation = await responseLocation.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<List<Data.Models.Location>>(apidataLocation);

            // update quantity -- OK
            foreach (var item in cartDetailsByUser)
            {
                var shoeDetail = shoeDetails.FirstOrDefault(p => p.Id == item.IdShoeDetail);

                if (shoeDetail != null)
                {
                    int quantity = shoeDetail.AvailableQuantity -= item.Quantity;
                    string urlApiShoeUpdateQTT = $"https://localhost:7109/api/ShoeDetails/update-quantity-by-id?id={shoeDetail.Id}&quantity={quantity}";
                    // var content = new StringContent(JsonConvert.SerializeObject(shoeDetails), Encoding.UTF8, "application/json");
                    var responseUpdateShoe = await _httpClient.PutAsync(urlApiShoeUpdateQTT, null);
                }
            }

            
            var location = locations.FirstOrDefault(p => p.Stage == tinh && p.District == huyen && p.Ward == xa && p.Street == duong && p.Address == diachi);
            // Create billl ---- OKK
            Bills newBill = new Bills();
            newBill.Id = Guid.NewGuid();
            newBill.IdUser = User.Id;
            newBill.CreateDate = DateTime.Now;
            newBill.Note = "ABC";
            newBill.Status = 1;
            newBill.IdCoupon = idCoupon;
            newBill.IdShipAdressMethod = idShipMethod;
            newBill.IdLocation = location.Id;
            newBill.IdPaymentMethod = idPaymentMethod;
            string apiURLCreateBill = $"https://localhost:7109/api/Bill/CreateBill?idBill={newBill.Id}&IdUser={User.Id}&Note=abc&status=1&IdCoupon={idCoupon}&IdShipMethod={idShipMethod}&IdLocation={location.Id}&IdPaymentMethod={idPaymentMethod}";
            var responseCreateBill = await _httpClient.PostAsync(apiURLCreateBill, null);

            //Crete BIllDT
            foreach (var item in cartDetailsByUser)
            {
                BillDetails newBillDetails = new BillDetails();
                newBillDetails.IdShoeDetail = item.IdShoeDetail;
                newBillDetails.IdBill = newBill.Id;
                var shoeDetail = shoeDetails.FirstOrDefault(p => p.Id == item.IdShoeDetail);
                newBillDetails.Price = shoeDetail.SellPrice;
                newBillDetails.Quantity = item.Quantity;
                string apiURLCreateBillDT = $"https://localhost:7109/api/BillDetails?IdShoeDetail={item.IdShoeDetail}&IdBill={newBillDetails.IdBill}&price={shoeDetail.SellPrice}&quantity={item.Quantity}";
                var responseCreateBillDT = await _httpClient.PostAsync(apiURLCreateBillDT, null);            
            }

            
            // Clear Cart --- OK
            for (int i = 0; i < cartDetailsByUser.Count; i++)
            {
                string apiURLCartDelete = $"https://localhost:7109/api/CartDetail/{cartDetailsByUser[i].Id}";
                var responseCleanCart = await _httpClient.DeleteAsync(apiURLCartDelete);    
            }

            return Content("thanh toan ok");
        }
    }

}
