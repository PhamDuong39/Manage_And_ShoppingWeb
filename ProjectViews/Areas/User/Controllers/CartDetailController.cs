using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectViews.Areas.User.Models;
using ProjectViews.Models;
using System.Text;

namespace ProjectViews.Areas.User.Controllers
{
    [Area("User")]
    public class CartDetailController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IAllRepositories<CartDetails> _cartRepository;
        private readonly IAllRepositories<ShoeDetails> _shoeRepository;
        private readonly AppDbContext _dbContext;

        public CartDetailController()
        {
            _httpClient = new HttpClient();
            _dbContext = new AppDbContext();
            _cartRepository = new AllRepositories1<CartDetails>(_dbContext, _dbContext.CartDetails);
            _shoeRepository = new AllRepositories1<ShoeDetails>(_dbContext, _dbContext.ShoeDetails);

        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {

            string apiURL = $"https://localhost:7109/api/CartDetail";
            string apiUrlShoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            string apiImage = $"https://localhost:7109/api/Images/get-all-image";
            string apiSize_shoe = $"https://localhost:7109/api/SIzes_ShoeDetails/get-all-size-shoe-details";
            string apiSize = $"https://localhost:7109/api/Size/get-all-size";

            var response = await _httpClient.GetAsync(apiURL);
            var reponce2 = await _httpClient.GetAsync(apiUrlShoe);
            var reponce3 = await _httpClient.GetAsync(apiImage);
            var reponce4 = await _httpClient.GetAsync(apiSize_shoe);
            var reponce5 = await _httpClient.GetAsync(apiSize);

            var apiData = await response.Content.ReadAsStringAsync();
            var apiDataShoe = await reponce2.Content.ReadAsStringAsync();
            var apiDataImage = await reponce3.Content.ReadAsStringAsync();
            var apiDataSize_SHoe = await reponce4.Content.ReadAsStringAsync();
            var apiDataSize = await reponce5.Content.ReadAsStringAsync();

            var cartdetail = JsonConvert.DeserializeObject<List<CartDetails>>(apiData);
            var shoes = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDataShoe);
            var image = JsonConvert.DeserializeObject<List<Images>>(apiDataImage);
            var size_shoe = JsonConvert.DeserializeObject<List<Sizes_ShoeDetails>>(apiDataSize_SHoe);
            var size = JsonConvert.DeserializeObject<List<Sizes>>(apiDataSize);

            ViewData["Name"] = new SelectList(shoes, "Id", "Name");
            ViewData["SellPrice"] = new SelectList(shoes, "Id", "SellPrice");

            List<CartDetailModel> lstModel = new List<CartDetailModel>();
            //int sum = 0;
            foreach (var item in cartdetail)
            {
                CartDetailModel model = new CartDetailModel();
                model.cartDetail = item;
                model.Id = item.Id;
                model.quantity = item.Quantity;
                var shoe = shoes.FirstOrDefault(s => s.Id == item.IdShoeDetail);
                model.name = shoe.Name;
                model.price = shoe.SellPrice;
                model.ToTalPrice = item.Quantity * shoe.SellPrice;
                var imageShoe = image.FirstOrDefault(p => p.IdShoeDetail == item.IdShoeDetail);
                model.ImageSource = imageShoe.ImageSource;

                var siZe_SHoe = size_shoe.FirstOrDefault(p => p.IdShoeDetails == item.IdShoeDetail);
                var sizes = size.FirstOrDefault(p => p.Id == siZe_SHoe.IdSize);
                model.Size = sizes.SizeNumber;
                lstModel.Add(model);
            }

            return View(lstModel);
        }
        //public async Task<IActionResult> Details(Guid Id)
        //{
        //    string apiURL = $"https://localhost:7109/api/CartDetail/{Id}";
        //    var response = await _httpClient.GetAsync(apiURL);
        //    var apiData = await response.Content.ReadAsStringAsync();
        //    var cartdetail = JsonConvert.DeserializeObject<CartDetails>(apiData);
        //    return View(cartdetail);
        //}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CartDetails cartDetails)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/create-cartdetail?IdUser={cartDetails.IdUser}&IdShoesDetail={cartDetails.IdShoeDetail}&Quantity={cartDetails.Quantity}";
            var content = new StringContent(JsonConvert.SerializeObject(cartDetails), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var cartdetail = JsonConvert.DeserializeObject<CartDetails>(apiData);
            return View(cartdetail);
        }
        public async Task<IActionResult> Edit(Guid Id, CartDetails cartDetails)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/update-cartdetail?id={cartDetails.Id}&idShoeDetail={cartDetails.IdShoeDetail}&idUser={cartDetails.IdUser}&quantity={cartDetails.Quantity}";

            var content = new StringContent(JsonConvert.SerializeObject(cartDetails), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete2(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/{Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            string username = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(username))
            {
                TempData["Message"] = "Vui lòng đăng nhập để thanh toán";
                return RedirectToAction("Show");
            }
            string apiUrl = "https://localhost:7109/api/User/get-all-user";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(apidata);

            var user = users.FirstOrDefault(p => p.Username == username);

            var cartDetails = _cartRepository.GetAll().Where(p => p.IdUser == user.Id).ToList();

            if (cartDetails.Count == 0)
            {
                TempData["Message"] = "Giỏ hàng không tồn tại";
                return RedirectToAction("Show");
            }

            foreach (var cartDetail in cartDetails)
            {
                var shoeDetail = _shoeRepository.GetAll().FirstOrDefault(p => p.Id == cartDetail.IdShoeDetail);
                if (shoeDetail != null)
                {
                    shoeDetail.AvailableQuantity -= cartDetail.Quantity;
                    _shoeRepository.Update(shoeDetail);
                }
            }

            foreach (var cartDetail in cartDetails)
            {
                _cartRepository.Delete(cartDetail);
            }

            TempData["Message"] = "Thanh toán thành công";
            return RedirectToAction("Show");
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(CartViewModel a)
        {
            string username = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty(username))
            {
                TempData["Message"] = "Vui lòng đăng nhập để thêm vào giỏ hàng";
                return RedirectToAction("Show");
            }

            string apiUrlUser = "https://localhost:7109/api/User/get-all-user";
            string apiUrlShoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            string apiURLCartdetail = $"https://localhost:7109/api/CartDetail";

            var responseUser = await _httpClient.GetAsync(apiUrlUser);
            var responseShoe = await _httpClient.GetAsync(apiUrlShoe);
            var responseCartdetail = await _httpClient.GetAsync(apiURLCartdetail);

            string apidataUser = await responseUser.Content.ReadAsStringAsync();
            string apidataShoe = await responseShoe.Content.ReadAsStringAsync();
            string apidataCartDetail = await responseCartdetail.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<List<Users>>(apidataUser);
            var shoe = JsonConvert.DeserializeObject<List<ShoeDetails>>(apidataShoe);
            var cartdetail = JsonConvert.DeserializeObject<List<CartDetails>>(apidataCartDetail);

            var shoes = shoe.FirstOrDefault(p => p.Id == a.IdShoe);
            var uSer = users.FirstOrDefault(p => p.Username == username);
            if(uSer != null && shoe != null)
            {
                var cartDetail = new CartDetails
                {
                    IdUser = uSer.Id,
                    IdShoeDetail = shoes.Id,
                    Quantity = 1,
                };
            }
            string apiUrl = $"https://localhost:7109/api/CartDetail/create-cartdetail";
            var content = new StringContent(JsonConvert.SerializeObject(cartdetail), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }

    }
}
