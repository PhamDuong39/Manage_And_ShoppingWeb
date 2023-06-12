using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class RoleController : Controller
    {
        private HttpClient _httpClient;
        public RoleController()
        {
            _httpClient = new HttpClient();
        }
        // GET: RoleController
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/Role/get-all-role";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<Roles>>(apiData);
            return View(roles);
        }

        // GET: RoleController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Role/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<Roles>(apiData);
            return View(roles);
        }

        // GET: RoleController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Roles roles)
        {
            string apiURL = $"https://localhost:7109/api/Role/create-role?RoleName={roles.RoleName}&Status={roles.Status = 1}";
            var content = new StringContent(JsonConvert.SerializeObject(roles), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }
        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Role/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<Roles>(apiData);
            return View(roles);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, Roles roles)
        {
            string apiURL = $"https://localhost:7109/api/Role/Edit-role-by-id?Id={Id}&RoleName={roles.RoleName}&Status={roles.Status}";
            var content = new StringContent(JsonConvert.SerializeObject(roles), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }


        // POST: RoleController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/Role/delete-role-by-id?Id={Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
