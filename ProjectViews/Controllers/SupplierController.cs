using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Controllers
{
    public class SupplierController : Controller
    {
        private readonly HttpClient _httpClient;

        public SupplierController()
        {
            _httpClient = new HttpClient();
        }

        //show
        public async Task<IActionResult> Show()
        {
            string apiUrl = "https://localhost:7109/api/Suppliers/get-all-supplier";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var sp = JsonConvert.DeserializeObject<IEnumerable<Supplier>>(apidata);
            return View(sp);
        }
        //create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            var content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"https://localhost:7109/api/Suppliers/create-supplier?addressSupplier={supplier.Address}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }

        //details
        public async Task<IActionResult> Detail(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7109/api/Suppliers/get-supplier-by-id?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var supplier = JsonConvert.DeserializeObject<Supplier>(result);
            if (response.IsSuccessStatusCode)
            {
                return View(supplier);
            }
            return NotFound();
        }

        //update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            //get supplier by id
            var response =
                await _httpClient.GetAsync($"https://localhost:7109/api/Suppliers/get-supplier-by-id?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var supplier = JsonConvert.DeserializeObject<Supplier>(result);
            if (response.IsSuccessStatusCode)
            {
                return View(supplier);
            }

            return this.RedirectToAction("Show");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Supplier supplier)
        {
            var content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7109/api/Suppliers/update-supplier?id={supplier.Id}&address={supplier.Address}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }

            return View();
        }

        //delete
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7109/api/Suppliers/delete-supplier?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }

            return NotFound();
        }
    }
}