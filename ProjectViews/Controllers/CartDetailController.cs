using Data.Models;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectViews.Models;
using System.Text;

namespace ProjectViews.Controllers
{
    public class CartDetailController : Controller
    {
        private readonly HttpClient _httpClient; 
        AppDbContext DbContext;
        public CartDetailController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> Show()
        {
            
            string apiURL = $"https://localhost:7109/api/CartDetail";
            string apiUrlShoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";

            var response = await _httpClient.GetAsync(apiURL);
            var reponce2 = await _httpClient.GetAsync(apiUrlShoe);

            var apiData = await response.Content.ReadAsStringAsync();
            var apiDataShoe = await reponce2.Content.ReadAsStringAsync();

            var cartdetail = JsonConvert.DeserializeObject<List<CartDetails>>(apiData);
            var shoes = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDataShoe);

            ViewData["Name"] = new SelectList(shoes, "Id", "Name");
            ViewData["SellPrice"] = new SelectList(shoes, "Id", "SellPrice");

            List<CartDetailModel> lstModel = new List<CartDetailModel>();
            //int sum = 0;
            foreach (var item in cartdetail)
            {
                CartDetailModel model = new CartDetailModel();
                model.cartDetail = item;
                model.Id = item.Id;
                model.quantity = item.Quantity;
                var shoe = shoes.FirstOrDefault(s => s.Id == item.IdShoeDetail);
                model.name = shoe.Name;
                model.price = shoe.SellPrice;
                model.ToTalPrice = item.Quantity * shoe.SellPrice;

                lstModel.Add(model);
            }

            return View(lstModel);
        }
        //public async Task<IActionResult> Details(Guid Id)
        //{
        //    string apiURL = $"https://localhost:7109/api/CartDetail/{Id}";
        //    var response = await _httpClient.GetAsync(apiURL);
        //    var apiData = await response.Content.ReadAsStringAsync();
        //    var cartdetail = JsonConvert.DeserializeObject<CartDetails>(apiData);
        //    return View(cartdetail);
        //}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CartDetails cartDetails)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/create-cartdetail?IdUser={cartDetails.IdUser}&IdShoesDetail={cartDetails.IdShoeDetail}&Quantity={cartDetails.Quantity}";
            var content = new StringContent(JsonConvert.SerializeObject(cartDetails), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var cartdetail = JsonConvert.DeserializeObject<CartDetails>(apiData);
            return View(cartdetail);
        }
        public async Task<IActionResult> Edit(Guid Id, CartDetails cartDetails)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/update-cartdetail?id={cartDetails.Id}&idShoeDetail={cartDetails.IdShoeDetail}&idUser={cartDetails.IdUser}&quantity={cartDetails.Quantity}";

            var content = new StringContent(JsonConvert.SerializeObject(cartDetails), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/CartDetail/{Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
