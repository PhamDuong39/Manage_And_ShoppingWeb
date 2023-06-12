using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;
        public CartController()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/Carts";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<List<Carts>>(apiData);

            return View(cart);
        }
        //[HttpGet]
        //public async Task<IActionResult> Details(Guid Id)
        //{
        //    string aipUrlCart = $"https://localhost:7109/api/Carts/{Id}";
        //}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Carts carts)
        {
            string apiURL = $"https://localhost:7109/api/Carts/create-carts?id={carts.IdUser}";
            var content = new StringContent(JsonConvert.SerializeObject(carts), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    string apiURL = $"https://localhost:7109/api/Carts/{id}";
        //    var response = await _httpClient.GetAsync(apiURL);
        //    var apiData = await response.Content.ReadAsStringAsync();
        //    var carts = JsonConvert.DeserializeObject<Carts>(apiData);
        //    return View(carts);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(Guid Id, Carts carts)
        //{
        //    string apiURL = $"https://localhost:7109/api/Carts/update-carts?id={carts.IdUser}";

        //    var content = new StringContent(JsonConvert.SerializeObject(carts), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PutAsync(apiURL, content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return this.RedirectToAction("Show");
        //    }
        //    return this.View();
        //}
       
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Carts/delete?id={Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
