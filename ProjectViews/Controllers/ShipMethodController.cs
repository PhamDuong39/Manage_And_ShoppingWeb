using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class ShipMethodController : Controller
    {
        private readonly HttpClient _httpClient;

        public ShipMethodController()
        {
            _httpClient = new HttpClient();
        }
        // GET: ShipMethodController
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/ShipMethod";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var shipmethods = JsonConvert.DeserializeObject<List<ShipAdressMethod>>(apiData);
            return View(shipmethods);
        }

        // GET: ShipMethodController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/ShipMethod/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var shipMethod = JsonConvert.DeserializeObject<ShipAdressMethod>(apiData);
            return View(shipMethod);
        }

        // GET: ShipMethodController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShipMethodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShipAdressMethod shipAdressMethod)
        {
            string apiURL = $"https://localhost:7109/api/ShipMethod/CreateShipMethod?Name={shipAdressMethod.NameAddress}&status={shipAdressMethod.Status}&price={shipAdressMethod.Price}";
            var content = new StringContent(JsonConvert.SerializeObject(shipAdressMethod), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }

        // GET: ShipMethodController/Edit/5
        public async Task<IActionResult> Edit(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/ShipMethod/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var shipMethod = JsonConvert.DeserializeObject<ShipAdressMethod>(apiData);
            return View(shipMethod);
        }

        // POST: ShipMethodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, ShipAdressMethod shipAdressMethod)
        {
            string apiURL = $"https://localhost:7109/api/ShipMethod/UpdateShipMethod?Id={Id}&Name={shipAdressMethod.NameAddress}&status={shipAdressMethod.Status}&price={shipAdressMethod.Price}";
            var content = new StringContent(JsonConvert.SerializeObject(shipAdressMethod), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL , content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }


        // POST: ShipMethodController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/ShipMethod/DeleteShipMethod?Id={Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
