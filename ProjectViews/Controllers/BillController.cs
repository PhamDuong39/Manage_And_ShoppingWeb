using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using ProjectViews.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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

            // kiem tra gia tri cua page. Neu page k co gia tri thi pageNumber = 1. Neu co thi pageNumber = page
            int pageNumber = (page ?? 1);

            // Hiuen thi voi gia tri mac dinh cua so phan tu trong moi trang. Hooc co the tuy chon
            int itemsPerPage = pageSize ?? defaultPageSize;

            string apiURL = $"https://localhost:7109/api/Bill";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var bills = JsonConvert.DeserializeObject<List<Bills>>(apiData);

            PagedResult<Bills> pagedResult = PaginationExtension.GetPagedData(bills, pageNumber, itemsPerPage);

            // Tinh toan du lieu cho moi trang
            ViewBag.CurrentPage = pagedResult.CurrentPage;
            ViewBag.TotalPages = pagedResult.TotalPages;
            ViewBag.HasPreviousPage = pagedResult.HasPreviousPage;
            ViewBag.HasNextPage = pagedResult.HasNextPage;
            ViewBag.PreviousPage = pagedResult.CurrentPage - 1;
            ViewBag.NextPage = pagedResult.CurrentPage + 1;

            string apiURLuser = $"https://localhost:7109/api/User/get-all-user";
            var responseGetuser = await _httpClient.GetAsync(apiURLuser);
            string apiDatauser = await responseGetuser.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<Users>>(apiDatauser);
            ViewData["lstUser"] = new SelectList(user, "Id", "Username");


            return View(pagedResult.Data);
        }

        // GET: BillController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURLGetBill = $"https://localhost:7109/api/Bill/{Id}";
            string apiURLGetBillDetail = $"https://localhost:7109/api/BillDetails/FillterByID/{Id}";

            var responseGetBill = await _httpClient.GetAsync(apiURLGetBill);
            var responseGetBillDT = await _httpClient.GetAsync(apiURLGetBillDetail);

            string apiDataBill = await responseGetBill.Content.ReadAsStringAsync();
            string apiDataBillDT = await responseGetBillDT.Content.ReadAsStringAsync();

            var bill = JsonConvert.DeserializeObject<Bills>(apiDataBill);
            var billDTs = JsonConvert.DeserializeObject<List<BillDetails>>(apiDataBillDT);

            // Get coupon
            var apiURLCoupon = $"https://localhost:7109/api/Coupons/{bill.IdCoupon}";
            var responseGetCoupon = await _httpClient.GetAsync(apiURLCoupon);
            string apiDataCoupon = await responseGetCoupon.Content.ReadAsStringAsync();
            var coupon = JsonConvert.DeserializeObject<Coupons>(apiDataCoupon);

            // show username by id
            string apiURLuser = $"https://localhost:7109/api/User/get-all-user";
            var responseGetuser = await _httpClient.GetAsync(apiURLuser);
            string apiDatauser = await responseGetuser.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<Users>>(apiDatauser);
            ViewData["lstUser"] = new SelectList(user, "Id", "Username");

            // location
            string apiURLLocation = $"https://localhost:7109/api/Location";
            var responseGetLocation = await _httpClient.GetAsync(apiURLLocation);
            string apiDataLocation = await responseGetLocation.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<List<Data.Models.Location>>(apiDataLocation);

            List<SelectListItem> selectListLocation = new List<SelectListItem>();
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
            string apiURLPayment = $"https://localhost:7109/api/PaymentMethod";
            var responseGetPayment = await _httpClient.GetAsync(apiURLPayment);
            string apiDataPayment = await responseGetPayment.Content.ReadAsStringAsync();
            var paymentMethod = JsonConvert.DeserializeObject<List<PaymentMethods>>(apiDataPayment);
            ViewData["lstPaymentmethod"] = new SelectList(paymentMethod, "Id", "NameMethod");

            // ship method
            string apiURLShip = $"https://localhost:7109/api/ShipMethod";
            var responseGetShip = await _httpClient.GetAsync(apiURLShip);
            string apiDataShip = await responseGetShip.Content.ReadAsStringAsync();
            var shipMethod = JsonConvert.DeserializeObject<List<ShipAdressMethod>>(apiDataShip);
            ViewData["lstShipmethod"] = new SelectList(shipMethod, "Id", "NameAddress");

            // Shoe detail
            string apiURLShoes = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            var responseGetShoes = await _httpClient.GetAsync(apiURLShoes);
            string apiDataShoees = await responseGetShoes.Content.ReadAsStringAsync();
            var shoes = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDataShoees);
            ViewData["lstShoes"] = new SelectList(shoes, "Id", "Name");

            BillsViewModel billViewMD = new BillsViewModel();
            int price = 0;
            foreach (var item in billDTs)
            {
                price += item.Price * item.Quantity;
            }

            billViewMD.bill = bill;
            billViewMD.lstBillDT = billDTs;

            billViewMD.DiscountMoney = (double)price * (Convert.ToDouble(coupon.DiscountValue) * 0.01);

            billViewMD.deliveryFee = shipMethod.FirstOrDefault(p => p.Id == bill.IdShipAdressMethod).Price;
            billViewMD.sumPrice = ((double)price + shipMethod.FirstOrDefault(p => p.Id == bill.IdShipAdressMethod).Price) - (double)price * (Convert.ToDouble(coupon.DiscountValue) * 0.01);
            billViewMD.NoDiscountPrice = ((double)price + shipMethod.FirstOrDefault(p => p.Id == bill.IdShipAdressMethod).Price);
            return View(billViewMD);
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

            string apiURL = $"https://localhost:7109/api/Bill/CreateBill?IdUser={bill.IdUser}&Note={bill.Note}&status={bill.Status}&IdCoupon={bill.IdCoupon}&IdShipMethod={bill.IdShipAdressMethod}&IdLocation={bill.IdLocation}&IdPaymentMethod={bill.IdPaymentMethod}";
            var content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);

            string apiURLuser = $"https://localhost:7109/api/User/get-all-user";
            var responseGetuser = await _httpClient.GetAsync(apiURLuser);
            string apiDatauser = await responseGetuser.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<List<Users>>(apiDatauser);
            ViewBag.IdUser = user;


            if (response.IsSuccessStatusCode)
            {
                // Bill created successfully
                return this.RedirectToAction("Show");
            }

            return this.View();

        }

        // GET: BillController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Bill/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var bills = JsonConvert.DeserializeObject<Bills>(apiData);
            return View(bills);

        }

        // POST: BillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, Bills bill)
        {
            string apiURL = $"https://localhost:7109/api/Bill/UpdateBill?id={Id}&IdUser={bill.IdUser}&Note={bill.Note}&status={bill.Status}&IdCoupon={bill.IdCoupon}&IdShipMethod={bill.IdShipAdressMethod}&IdLocation={bill.IdLocation}&IdPaymentMethod={bill.IdPaymentMethod}";
            var content = new StringContent(JsonConvert.SerializeObject(bill), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL , content);

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
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}