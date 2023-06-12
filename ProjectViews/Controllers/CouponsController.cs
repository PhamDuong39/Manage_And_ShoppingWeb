using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class CouponsController:Controller
    {
        private readonly HttpClient _httpClient;

        public CouponsController()
        {
            _httpClient = new HttpClient();
        }

        // GET: PaymentMethodController
        [HttpGet]
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/Coupons/Show-Coupons";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<List<Coupons>>(apiData);
            return View(coupons);
        }

        // GET: PaymentMethodController/Details/5
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Coupons/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<Coupons>(apiData);
            return View(coupons);
        }

        // GET: PaymentMethodController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethodController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Coupons coupons)
        {
            if (!IsHanSuDungHopLe(coupons.TimeEnd))
            {
                ModelState.AddModelError("", "Hạn sử dụng phải lớn hơn 6 giờ.");
                return View(coupons);
            }
            string apiURL = $"https://localhost:7109/api/Coupons/Create-Coupons?DiscountValue={coupons.DiscountValue}&Quantity={coupons.Quantity}&VoucherName={coupons.VoucherName}";
            var content = new StringContent(JsonConvert.SerializeObject(coupons), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        private bool IsHanSuDungHopLe(DateTime hanSuDung)
        {
            DateTime hienTai = DateTime.Now;
            TimeSpan thoiGianConLai = hanSuDung - hienTai;
            if (thoiGianConLai.TotalHours <= 6)
            {
                return false;
            }
            return true;
        }

        [HttpGet]

        // GET: PaymentMethodController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/Coupons/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var coupons = JsonConvert.DeserializeObject<Coupons>(apiData);
            return View(coupons);
        }

        // POST: PaymentMethodController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, Coupons coupons)
        {
            string formattedEndDate = coupons.TimeEnd.ToString("yyyy-MM-ddTHH:mm:ss");
            string apiURL = $"https://localhost:7109/api/Coupons/edit-Coupons-{Id}?DiscountValue={coupons.DiscountValue}&Quantity={coupons.Quantity}&VoucherName={coupons.VoucherName}&TimeStart={coupons.TimeStart}&TimeEnd={formattedEndDate}";

            var content = new StringContent(JsonConvert.SerializeObject(coupons), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }

        // POST: PaymentMethodController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Coupons/Delete-Coupons-/{Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
