using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class FeedBacksController : Controller
    {
        private HttpClient _httpClient;
        public FeedBacksController()
        {
            _httpClient = new HttpClient();
        }
        // GET: FeedBacksController
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/Feedbacks/get-all-feedbacks";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var feedbacks = JsonConvert.DeserializeObject<List<Feedbacks>>(apiData);
            return View(feedbacks);
        }

        // GET: FeedBacksController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Feedbacks/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var feedbacks = JsonConvert.DeserializeObject<Feedbacks>(apiData);
            return View(feedbacks);
        }

        // GET: FeedBacksController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeedBacksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedbacks feedbacks)
        {
            string apiURL = $"https://localhost:7109/api/Feedbacks/create-feedbacks?IdUser={feedbacks.IdUser}&IdShoeDetail={feedbacks.IdShoeDetail}&Note={feedbacks.Note}&RatingStar={feedbacks.RatingStar}";
            var content = new StringContent(JsonConvert.SerializeObject(feedbacks), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }
        // GET: FeedBacksController/Edit/5
        public async Task<IActionResult> Edit(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Feedbacks/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var feedbacks = JsonConvert.DeserializeObject<Feedbacks>(apiData);
            return View(feedbacks);
        }

        // POST: FeedBacksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, Feedbacks feedbacks)
        {
            string apiURL = $"https://localhost:7109/api/Feedbacks/edit-feedbacks-by-id?Id={Id}&IdUser={feedbacks.IdUser}&IdShoeDetail={feedbacks.IdShoeDetail}&Note={feedbacks.Note}&RatingStar={feedbacks.RatingStar}";
            var content = new StringContent(JsonConvert.SerializeObject(feedbacks), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }


        // GET: FeedBacksController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Feedbacks/delete-feedbacks-by-id?Id={Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
