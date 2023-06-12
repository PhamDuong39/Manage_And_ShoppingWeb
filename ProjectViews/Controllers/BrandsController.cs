using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Controllers
{
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;

        public BrandsController()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<IActionResult> Show()
        {
            string apiUrl = $"https://localhost:7109/api/Brands/get-all-brands";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var brd = JsonConvert.DeserializeObject<IEnumerable<Brands>>(apidata);
            return View(brd);
        }

        public IActionResult Create()
        {
            return View();
        }

        //create
        [HttpPost]
        public async Task<IActionResult> Create(Brands brands)
        {
            string apiUrl = $"https://localhost:7109/api/Brands/create-brand?brandName={brands.Name}";
            var content = new StringContent(JsonConvert.SerializeObject(brands), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }

            return this.View();
        }

        //details
        public async Task<IActionResult> Detail(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Brands/get-brand-by-id?id={id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();

            var brd = JsonConvert.DeserializeObject<Brands>(apidata);
            return View(brd);
        }

        //update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Brands/get-brand-by-id?id={id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();

            var brd = JsonConvert.DeserializeObject<Brands>(apidata);
            return View(brd);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Brands brands)
        {
            string apiUrl =
                $"https://localhost:7109/api/Brands/update-brand-by-id?id={brands.Id}&brandName={brands.Name}";
            var content = new StringContent(JsonConvert.SerializeObject(brands), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }

            return this.View();
        }

        //delete
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Brands/delete-brands-by-id?id={id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }

            return this.RedirectToAction("Show");
        }

        public async Task<IActionResult> DeleteManyBrands(List<Guid> id)
        {
            try
            {
                if (id != null && id.Count > 0)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var url = "https://localhost:7109/api/Brands/delete-many-brands";
                        var jsonString = JsonConvert.SerializeObject(id);
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                        // Send the DELETE request
                        using (var response = await httpClient.DeleteAsync(url))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                // Handle the successful response
                                return RedirectToAction("Show");
                            }
                            else
                            {
                                // Handle the error response
                                var errorResponse = await response.Content.ReadAsStringAsync();
                                Console.WriteLine("Error deleting brands: " + errorResponse);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No brands to delete.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return RedirectToAction("Show");
        }
    }
}