using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Composition.Convention;
using System.Text;

namespace ProjectViews.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentMethodController()
        {
            _httpClient = new HttpClient();
        }

        // GET: PaymentMethodController
        [HttpGet]
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/PaymentMethod";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var paymentMethod = JsonConvert.DeserializeObject<List<PaymentMethods>>(apiData);
            return View(paymentMethod);
        }

        // GET: PaymentMethodController/Details/5
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/PaymentMethod/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var paymentMethod = JsonConvert.DeserializeObject<PaymentMethods>(apiData);
            return View(paymentMethod);
        }

        // GET: PaymentMethodController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethods paymentMethods)
        {
            string apiURL = $"https://localhost:7109/api/PaymentMethod/CreatePaymentMethod?status={paymentMethods.Status}&Name={paymentMethods.NameMethod}";
            var content = new StringContent(JsonConvert.SerializeObject(paymentMethods), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");   
            }
            return this.View();
        }

        // GET: PaymentMethodController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/PaymentMethod/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var paymentMethod = JsonConvert.DeserializeObject<PaymentMethods>(apiData);
            return View(paymentMethod);
        }

        // POST: PaymentMethodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, PaymentMethods paymentMethod)
        {
            string apiURL = $"https://localhost:7109/api/PaymentMethod/UpdatePaymentMethod?id={Id}&status={paymentMethod.Status}&Name={paymentMethod.NameMethod}";
           
            var content = new StringContent(JsonConvert.SerializeObject(paymentMethod), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }

        // POST: PaymentMethodController/Delete/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/PaymentMethod/Delete?id={Id}";
            var response = await _httpClient.DeleteAsync(apiURL) ;
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
