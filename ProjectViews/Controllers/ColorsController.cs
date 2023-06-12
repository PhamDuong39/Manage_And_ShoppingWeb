using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Controllers
{
    using Data.Models;
    using System.Text;

    public class ColorsController : Controller
    {
        private readonly HttpClient _httpClient;
        public ColorsController()
        {
            this._httpClient = new HttpClient();
        }
        public async Task<IActionResult> Show()
        {
            string apiUrl = "https://localhost:7109/api/Color/get-all-colors";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var clor = JsonConvert.DeserializeObject<IEnumerable<Colors>>(apidata);
            return View(clor);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Colors colors)
        {
            // Validate the location object and perform necessary checks
            // Call the API to create the location
            string apiUrl = $"https://localhost:7109/api/Color/create-color?colorName={colors.ColorName.TrimStart('#')}";
            var content = new StringContent(JsonConvert.SerializeObject(colors), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            // Check the response and handle success/failure accordingly
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }else
            {
                return View(colors);
            }
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Color/get-color-by-id/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();

            var clor = JsonConvert.DeserializeObject<Colors>(apidata);
            return View(clor);
        }
        //tao view update

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            string apiUrl = $"https://localhost:7109/api/Color/get-color-by-id/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var clor = JsonConvert.DeserializeObject<Colors>(apidata);
            return this.View(clor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Colors clors)
        {
            string apiUrl = $"https://localhost:7109/api/Color/update-color-by-id?Id={clors.Id}&colorName={clors.ColorName.TrimStart('#')}";
            var content = new StringContent(JsonConvert.SerializeObject(clors), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }

            return this.View(clors);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Color/delete-color-by-id?Id={id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }

    }
}
