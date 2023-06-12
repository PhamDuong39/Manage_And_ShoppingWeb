using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using ProjectViews.Models;
namespace ProjectViews.Controllers
{
    public class SizeController : Controller
    {
        private readonly HttpClient _httpClient;

        public SizeController()
        {
            this._httpClient = new HttpClient();
        }
        // show
        public async Task<IActionResult> ShowSize()
        {
            string apiURL = $"https://localhost:7109/api/Size/get-all-size";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<IEnumerable<Sizes>>(apiData);
            return  View(sizes);
        }
        public IActionResult CreateSize()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSize(Sizes sizes)
        {
            //Lay du lieu tu form gui len va tao moi
            string apiURL = $"https://localhost:7109/api/Size/create-size?sizeNumberCreate={sizes.SizeNumber}";
            var content = new StringContent(JsonConvert.SerializeObject(sizes), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("ShowSize");
            }
            return this.View(sizes);
        }
        //Details
        public async Task<IActionResult> DetailSize(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/Size/get-size-by-id/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<Sizes>(apiData);
            return View(sizes);
        }
        //Update
        [HttpGet]
        public async Task<IActionResult> UpdateSize(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/Size/get-size-by-id/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<Sizes>(apiData);
            return this.View(sizes);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSize(Sizes sizes)
        {
            string apiURL = $"https://localhost:7109/api/Size/update-size-by-id?id={sizes.Id}&sizeNumberUpdate={sizes.SizeNumber}";
            var content = new StringContent(JsonConvert.SerializeObject(sizes), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("ShowSize");
            }
            return this.View(sizes);
        }
        //Delete
        public async Task<IActionResult> DeleteSize(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/Size/delete-size-by-id?id={id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("ShowSize");
            }
            return this.RedirectToAction("ShowSize");
        }
    }
}
