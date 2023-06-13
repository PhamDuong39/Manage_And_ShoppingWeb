using Data.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            string apiURL = $"https://localhost:7109/api/User/Login?username={username}&password={password}";
            var response = await _httpClient.GetAsync(apiURL);
            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                return RedirectToAction("Login");
            }
            else
            {
                string apiURLRole = $"https://localhost:7109/api/Role/get-all-role";
                var responseRole = await _httpClient.GetAsync(apiURLRole);
                var apiDataRole = await responseRole.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<Roles>>(apiDataRole);

                string apiUrlUser = "https://localhost:7109/api/User/get-all-user";
                var responseUser = await _httpClient.GetAsync(apiUrlUser);
                string apidataUser = await responseUser.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<Users>>(apidataUser);

                var user = users.FirstOrDefault(p => p.Username == username);

                var role = roles.FirstOrDefault(p => p.Id == user.IdRole);

                if (role.RoleName == "Admin")
                {
                    HttpContext.Session.SetString("Admin", username);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    HttpContext.Session.SetString("User", username);
                    return RedirectToAction("Index", "Home", new { area = "User" });

                }

            }
        }

		public IActionResult LogOutUser()
		{
			HttpContext.Session.Remove("User");

			return RedirectToAction("Login");
		}

		public IActionResult LogOutAdmin()
		{
			HttpContext.Session.Remove("Admin");

			return RedirectToAction("Login");
		}

	}
}
