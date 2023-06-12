using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController()
        {
            _httpClient = new HttpClient();
        }

        //show
        public async Task<IActionResult> Show()
        {
            string apiUrl =$"https://localhost:7109/api/Categories/get-all-categories";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var cat = JsonConvert.DeserializeObject<IEnumerable<Categories>>(apidata);
            //check if cat is null
            return View(cat);
        }

        //create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categories categories)
        {
            string apiUrl = $"https://localhost:7109/api/Categories/create-category?categoryName={categories.CategoryName}";
            var content = new StringContent(JsonConvert.SerializeObject(categories), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(apiUrl, content);
            if (response.Result.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        //details
        public async Task<IActionResult> Detail(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Categories/get-categories-by-id?id={id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var cat = JsonConvert.DeserializeObject<Categories>(apidata);
            return View(cat);
        }
        //update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Categories/get-categories-by-id?id={id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var cat = JsonConvert.DeserializeObject<Categories>(apidata);
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Categories categories)
        {
            string apiUrl = $"https://localhost:7109/api/Categories/update-category?id={categories.Id}&categoryName={categories.CategoryName}";
            var content = new StringContent(JsonConvert.SerializeObject(categories), Encoding.UTF8, "application/json");
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
            string apiUrl = $"https://localhost:7109/api/Categories/delete-category?id={id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.Content("Hello, Khong xoa duoc");
        }
    }
}
