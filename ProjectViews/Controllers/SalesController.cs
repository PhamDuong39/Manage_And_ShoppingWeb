using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class SalesController : Controller
    {
        private readonly HttpClient _httpClient;

        public SalesController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/Sales/Show-Sales";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<Sales>>(apiData);


            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Sales/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<Sales>(apiData);
            return View(sales);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sales sales)
        {
            if (!IsHanSuDungHopLe(sales.EndDate))
            {
                ModelState.AddModelError("", "Hạn sử dụng phải lớn hơn 6 giờ.");
                return View(sales);
            }
            string apiURL = $"https://localhost:7109/api/Sales/Create-Sales?DiscountValue={sales.DiscountValue}&SaleName={sales.SaleName}";
            var content = new StringContent(JsonConvert.SerializeObject(sales), Encoding.UTF8, "application/json");
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
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/Sales/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<Sales>(apiData);
            return View(sales);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Sales sales)
        {
            string formattedEndDate = sales.EndDate.ToString("yyyy-MM-ddTHH:mm:ss");

            string apiURL = $"https://localhost:7109/api/Sales/EditSales?id={id}&DiscountValue={sales.DiscountValue}&SaleName={sales.SaleName}&EndDate={formattedEndDate}";
            var content = new StringContent(JsonConvert.SerializeObject(sales), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Sales/Delete-Sales-/{Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
