using Data.Models;
using Data.ShopContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
namespace ProjectViews.Controllers
{
    public class AchivePointsController : Controller
    {
        private HttpClient _httpClient;
        public AchivePointsController()
        {
            _httpClient = new HttpClient();
        }
        // GET: AchivePointController
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/AchivePoints/get-all-achivepoint";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var achivepoint = JsonConvert.DeserializeObject<IEnumerable<AchivePoint>>(apiData);
            return View(achivepoint);
        }
        // GET: AchivePointController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/AchivePoints/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var achivepoint = JsonConvert.DeserializeObject<AchivePoint>(apiData);

            return View(achivepoint);
        }

        // GET: AchivePointController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AchivePointController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AchivePoint achivePoint)
        {
            string apiURL = $"https://localhost:7109/api/AchivePoints/create-achivepoint?IdUser={achivePoint.IdUser}&PointValue={achivePoint.PointValue}";
            var content = new StringContent(JsonConvert.SerializeObject(achivePoint), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }

            return View();
        }
        // GET: AchivePointController/Edit/5
        public async Task<IActionResult> Edit(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/AchivePoints/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var achivepoint = JsonConvert.DeserializeObject<AchivePoint>(apiData);
            return View(achivepoint);
        }

        // POST: AchivePointController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, AchivePoint achivePoint)
        {
            string apiURL = $"https://localhost:7109/api/AchivePoints/update-achivepoint?Id={Id}&IdUser={achivePoint.IdUser}&PointValue={achivePoint.PointValue}";
            var content = new StringContent(JsonConvert.SerializeObject(achivePoint), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }

        // GET: AchivePointController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/AchivePoints/delete-achivepoint?Id={Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }

    }
}
