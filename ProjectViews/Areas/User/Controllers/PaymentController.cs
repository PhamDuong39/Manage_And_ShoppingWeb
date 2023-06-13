using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectViews.Models;

namespace ProjectViews.Areas.User.Controllers
{
	public class PaymentController : Controller
	{
		private readonly HttpClient _httpClient;

		public PaymentController()
		{
			_httpClient = new HttpClient();
		}

		[HttpGet]
		public async Task<IActionResult> Show()
		{
			// Lay Danh sach san pham
			string apiUrlshoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
			var responseshoe = await _httpClient.GetAsync(apiUrlshoe); // goi api lay data
			string apiDatashoe = await responseshoe.Content.ReadAsStringAsync(); // doc data tra ve
			var shoeDetails = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDatashoe);

			// Lay phuong thuc thanh toan
			string apiURLPayMethod = $"https://localhost:7109/api/PaymentMethod";
			var responsePayMethod = await _httpClient.GetAsync(apiURLPayMethod);
			var apiDataPayMethod = await responsePayMethod.Content.ReadAsStringAsync();
			var paymentMethod = JsonConvert.DeserializeObject<List<PaymentMethods>>(apiDataPayMethod);

			// Lay cac thong tin san co cua user
			string username = HttpContext.Session.GetString("User");
			string apiUrlUser = "https://localhost:7109/api/User/get-all-user";
			var responseUser = await _httpClient.GetAsync(apiUrlUser);
			string apidataUser = await responseUser.Content.ReadAsStringAsync();
			var users = JsonConvert.DeserializeObject<List<Users>>(apidataUser);
			var User = users.FirstOrDefault(p => p.Username == username);

			// Lay danh sach CartDetail de thanh toan
			string apiURLCartDetail = $"https://localhost:7109/api/CartDetail";
			var responseCartDetail = await _httpClient.GetAsync(apiURLCartDetail);
			var apiDataCartDetail = await responseCartDetail.Content.ReadAsStringAsync();
			var cartdetails = JsonConvert.DeserializeObject<List<CartDetails>>(apiDataCartDetail);

			// Lay danh sach cac Coupon
			string apiURL = $"https://localhost:7109/api/Coupons/Show-Coupons";
			var response = await _httpClient.GetAsync(apiURL);
			var apiData = await response.Content.ReadAsStringAsync();
			var coupons = JsonConvert.DeserializeObject<List<Coupons>>(apiData);

			// Ham hien thi so tien tam tinh
			foreach (var item in cartdetails)
			{

			}


			return Ok();
		}

		public async Task<IActionResult> ConfirmPay()
		{
			return Ok();
		}
	}
}
