using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectViews.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Xml.Linq;

namespace ProjectViews.Controllers
{
    public class UserController : Controller
    {
        private HttpClient _httpClient;
        public UserController()
        {
            _httpClient = new HttpClient();
        }
        // GET: UserController
        public async Task<IActionResult> Show()
        {
            string apiUrl = "https://localhost:7109/api/User/get-all-user";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(apidata);
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users users)
        {
            string apiUrl = $"https://localhost:7109/api/User/create-user?IdRole={users.IdRole}&Username={users.Username}&Password={users.Password}&Address={users.Address}&Phonenumber={users.Phonenumber}&Email={users.Email}&Status={users.Status}&Fullname={users.Fullname}";
            var content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/User/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();

            var user = JsonConvert.DeserializeObject<Users>(apidata);
            return View(user);
        }


        // GET: UserController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/User/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Users>(apidata);
            return View(user);
        }
        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid Id, Users users)
        {
            string apiUrl = $"https://localhost:7109/api/User/update-user-by-id?Id={Id}&IdRole={users.IdRole}&Username={users.Username}&Password={users.Password}&Address={users.Address}&Phonenumber={users.Phonenumber}&Email={users.Email}&Status={users.Status}&Fullname={users.Fullname}";
            var content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Edit2(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/User/{id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<Users>(apidata);
            return View(user);
        }
        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(Guid Id, Users users)
        {
            string apiUrl = $"https://localhost:7109/api/User/update-user-by-id?Id={Id}&IdRole={users.IdRole}&Username={users.Username}&Password={users.Password}&Address={users.Address}&Phonenumber={users.Phonenumber}&Email={users.Email}&Status={users.Status}&Fullname={users.Fullname}";
            var content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View(users);
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/User/delete-user-by-id?Id={id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
