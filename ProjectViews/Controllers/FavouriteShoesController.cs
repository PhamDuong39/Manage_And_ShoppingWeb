using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ProjectViews.Controllers
{
    public class FavouriteShoesController : Controller
    {
        private readonly HttpClient _httpClient;

        public FavouriteShoesController()
        {
            _httpClient = new HttpClient();
        }
        [HttpGet]
        public async Task<IActionResult> Show()
        {
            string apiURL = $"https://localhost:7109/api/FavouriteShoes";
            string apiUrlShoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";

            var response = await _httpClient.GetAsync(apiURL);
            var reponce2 = await _httpClient.GetAsync(apiUrlShoe);

            var apiData = await response.Content.ReadAsStringAsync();
            var apiDataShoe = await reponce2.Content.ReadAsStringAsync();

            var favs = JsonConvert.DeserializeObject<List<FavouriteShoes>>(apiData);
            var shoes = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDataShoe);

            ViewData["Name"] = new SelectList(shoes, "Id", "Name");
            return View(favs);
        }
        public async Task<IActionResult> Details(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/FavouriteShoes/{Id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var favs = JsonConvert.DeserializeObject<FavouriteShoes>(apiData);
            return View(favs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FavouriteShoes favouriteShoes)
        {
            string apiURL = $"https://localhost:7109/api/FavouriteShoes/create-favouriteshoes?idUser={favouriteShoes.IdUser}&idShoes={favouriteShoes.IdShoeDetail}&status={favouriteShoes.Status}";
            

            var content = new StringContent(JsonConvert.SerializeObject(favouriteShoes), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, content);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.View();
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiURL = $"https://localhost:7109/api/FavouriteShoes/{id}";
            var response = await _httpClient.GetAsync(apiURL);
            var apiData = await response.Content.ReadAsStringAsync();
            var favs = JsonConvert.DeserializeObject<FavouriteShoes>(apiData);
            return View(favs);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, FavouriteShoes favouriteShoes)
        {
            string apiURL = $"https://localhost:7109/api/FavouriteShoes/edit-favouriteshoes?id={Id}&idUser={favouriteShoes.IdUser}&idShoesDetail={favouriteShoes.IdShoeDetail}&status={favouriteShoes.Status}";

            var content = new StringContent(JsonConvert.SerializeObject(favouriteShoes), Encoding.UTF8, "application/json");
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
            string apiURL = $"https://localhost:7109/api/FavouriteShoes/{Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }
            return this.RedirectToAction("Show");
        }
    }
}
