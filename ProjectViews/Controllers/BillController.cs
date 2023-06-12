using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Newtonsoft.Json;
using System.Text;
using ProjectViews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectViews.Services;
using static ProjectViews.Services.PaginationExtension;

namespace ProjectViews.Controllers
{
    public class BillController : Controller
    {
        private readonly HttpClient _httpClient;
        public BillController()
        {
            _httpClient = new HttpClient();
        }
        // GET: BillController
        [HttpGet]
        public async Task<IActionResult> Show(int? page, int? pageSize)
        {
            int defaultPageSize = 5;

            //Check gia tri cua page. Neu page k co gia tri thi pageNumber = 1. Neu co thi pageNumber = page
            int pageNumber = (page ?? 1);

            // Show voi gia tri mac dinh cua so phan tu trong moi Page. or co the chose other value
            int itemsPerPage = pageSize ?? defaultPageSize;

            string apiUrl = $"https://localhost:7109/api/Bill";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var bills = JsonConvert.DeserializeObject<List<Bills>>(apiData);
            if (bills != null)
            {
                PagedResult<Bills> pagedResult = PaginationExtension.GetPagedData(bills, pageNumber, itemsPerPage);

                // Tinh toan du lieu cho page
                ViewBag.CurrentPage = pagedResult.CurrentPage;
                ViewBag.TotalPages = pagedResult.TotalPages;
                ViewBag.HasPreviousPage = pagedResult.HasPreviousPage;
                ViewBag.HasNextPage = pagedResult.HasNextPage;
                ViewBag.PreviousPage = pagedResult.CurrentPage - 1;
                ViewBag.NextPage = pagedResult.CurrentPage + 1;

                string apiUrUser = $"https://localhost:7109/api/User/get-all-user";
                var responseGetUser = await _httpClient.GetAsync(apiUrUser);
                string apiDataUser = await responseGetUser.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<List<Users>>(apiDataUser);
                ViewData["lstUser"] = new SelectList(user, "Id", "Username");


                return View(pagedResult.Data);
            }else
            {
                return Content("Bills Null");
            }
        }

        // GET: BillController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string apiUrlGetBill = $"https://localhost:7109/api/Bill/{id}";
            string apiUrlGetBillDetail = $"https://localhost:7109/api/BillDetails/FillterByID/{id}";

            var responseGetBill = await _httpClient.GetAsync(apiUrlGetBill);
            var responseGetBillDt = await _httpClient.GetAsync(apiUrlGetBillDetail);

            string apiDataBill = await responseGetBill.Content.ReadAsStringAsync();
            string apiDataBillDt = await responseGetBillDt.Content.ReadAsStringAsync();

            var bill = JsonConvert.DeserializeObject<Bills>(apiDataBill);
            var billDTs = JsonConvert.DeserializeObject<List<BillDetails>>(apiDataBillDt);

            // Get coupon
            if (bill != null)
            {
                var apiUrlCoupon = $"https://localhost:7109/api/Coupons/{bill.IdCoupon}";
                var responseGetCoupon = await _httpClient.GetAsync(apiUrlCoupon);
                string apiDataCoupon = await responseGetCoupon.Content.ReadAsStringAsync();
                var coupon = JsonConvert.DeserializeObject<Coupons>(apiDataCoupon);

                // show username by id
                string apiUrUser = $"https://localhost:7109/api/User/get-all-user";
                var responseGetUser = await _httpClient.GetAsync(apiUrUser);
                string apiDataUser = await responseGetUser.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<List<Users>>(apiDataUser);
                ViewData["lstUser"] = new SelectList(user, "Id", "Username");

                // location
                string apiUrlLocation = $"https://localhost:7109/api/Location";
                var responseGetLocation = await _httpClient.GetAsync(apiUrlLocation);
                string apiDataLocation = await responseGetLocation.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<List<Location>>(apiDataLocation);

                List<SelectListItem> selectListLocation = new List<SelectListItem>();
                if (location != null)
                    foreach (var item in location)
                    {
                        selectListLocation.Add(new SelectListItem
                        {
                            Value = item.Id.ToString(),
                            Text = item.Stage + "" + item.District + "" + item.Ward + "" + item.Street + "" + item.Address
                        });
                    }


                ViewData["lstLocation"] = selectListLocation;

                // Payment method
                string apiUrlPayment = $"https://localhost:7109/api/PaymentMethod";
                var responseGetPayment = await _httpClient.GetAsync(apiUrlPayment);
                string apiDataPayment = await responseGetPayment.Content.ReadAsStringAsync();
                var paymentMethod = JsonConvert.DeserializeObject<List<PaymentMethods>>(apiDataPayment);
                ViewData["lstPaymentmethod"] = new SelectList(paymentMethod, "Id", "NameMethod");

                // ship method
                string apiUrlShip = $"https://localhost:7109/api/ShipMethod";
                var responseGetShip = await _httpClient.GetAsync(apiUrlShip);
                string apiDataShip = await responseGetShip.Content.ReadAsStringAsync();
                var shipMethod = JsonConvert.DeserializeObject<List<ShipAdressMethod>>(apiDataShip);
                ViewData["lstShipmethod"] = new SelectList(shipMethod, "Id", "NameAddress");

                // Shoe detail
                string apiUrlShoes = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
                var responseGetShoes = await _httpClient.GetAsync(apiUrlShoes);
                string apiDataShoes = await responseGetShoes.Content.ReadAsStringAsync();
                var shoes = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDataShoes);
                ViewData["lstShoes"] = new SelectList(shoes, "Id", "Name");

                BillsViewModel billViewMd = new BillsViewModel();
                int price = 0;
                if (billDTs != null)
                {
                    foreach (var item in billDTs)
                    {
                        price += item.Price * item.Quantity;
                    }

                    billViewMd.bill = bill;
                    billViewMd.lstBillDT = billDTs;
                }

                if (coupon != null)
                {
                    billViewMd.DiscountMoney = Convert.ToDouble(price) * (Convert.ToDouble(coupon.DiscountValue) * 0.01);

                    if (shipMethod != null)
                    {
                        billViewMd.deliveryFee = shipMethod.FirstOrDefault(p => p.Id == bill.IdShipAdressMethod)!.Price;
                        billViewMd.sumPrice =
                            ((double)price + shipMethod.FirstOrDefault(p => p.Id == bill.IdShipAdressMethod)!.Price) -
                            Convert.ToDouble(price) * (Convert.ToDouble(coupon.DiscountValue) * 0.01);
                    }
                }

                if (shipMethod != null)
                    billViewMd.NoDiscountPrice =
                        ((double)price + shipMethod.FirstOrDefault(p => p.Id == bill.IdShipAdressMethod)!.Price);
                return View(billViewMd);
            }
            else
            {
                return Content("Bill Null");
            }
        }

        // GET: BillController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bills bill)
        {

            string apiUrl = $"https://localhost:7109/api/Bill/CreateBill?IdUser={bill.IdUser}&Note={bill.Note}&status={bill.Status}&IdCoupon={bill.IdCoupon}&IdShipMethod={bill.IdShipAdressMethod}&IdLocation={bill.IdLocation}&IdPaymentMethod={bill.IdPaymentMethod}";
            var content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            string apiUrUser = $"https://localhost:7109/api/User/get-all-user";
            var responseGetUser = await _httpClient.GetAsync(apiUrUser);
            string apiDataUser = await responseGetUser.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<Users>>(apiDataUser);
            if (user != null) ViewBag.IdUser = user;


            if (response.IsSuccessStatusCode)
            {
                // Bill created successfully
                return this.RedirectToAction("Show");
            }

            return this.View();

        }

        // GET: BillController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Bill/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var bills = JsonConvert.DeserializeObject<Bills>(apiData);
            return View(bills);

        }

        // POST: BillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Bills bill)
        {
            string apiUrl =
                $"https://localhost:7109/api/Bill/UpdateBill?id={id}&IdUser={bill.IdUser}&Note={bill.Note}&status={bill.Status}&IdCoupon={bill.IdCoupon}&IdShipMethod={bill.IdShipAdressMethod}&IdLocation={bill.IdLocation}&IdPaymentMethod={bill.IdPaymentMethod}";
            var content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiUrl , content);

            if (response.IsSuccessStatusCode)
            {
                // Bill updated successfully
                return this.RedirectToAction("Show");
            }

            return this.View();
        }



        // POST: BillController/Delete/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7109/api/Bill/DeleteBill/{id}";
                var response = await _httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                return BadRequest();
            }
            catch
            {
                return Ok("Error while processing your request");
            }
        }
    }
}