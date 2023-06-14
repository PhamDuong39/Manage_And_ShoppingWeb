using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectViews.Areas.User.Models;
using System.Text;

namespace ProjectViews.Areas.User.Controllers
{
    [Area("User")]
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
            ViewData["user"] = HttpContext.Session.GetString("User");
            string apiURL = $"https://localhost:7109/api/FavouriteShoes";
            string apiUrlShoe = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            string apiImage = $"https://localhost:7109/api/Images/get-all-image";

            var response = await _httpClient.GetAsync(apiURL);
            var reponce2 = await _httpClient.GetAsync(apiUrlShoe);
            var reponce3 = await _httpClient.GetAsync(apiImage);

            var apiData = await response.Content.ReadAsStringAsync();
            var apiDataShoe = await reponce2.Content.ReadAsStringAsync();
            var apiDataImage = await reponce3.Content.ReadAsStringAsync();

            var favs = JsonConvert.DeserializeObject<List<FavouriteShoes>>(apiData);
            var shoes = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDataShoe);
            var image = JsonConvert.DeserializeObject<List<Images>>(apiDataImage);
            
            List<FavouriteShoesModel> lstmodel = new List<FavouriteShoesModel>();
            foreach(var item in favs)
            {
                FavouriteShoesModel model = new FavouriteShoesModel();
                var images = image.FirstOrDefault(c => c.IdShoeDetail == item.IdShoeDetail);
                model.imageSource = images.ImageSource;
                var shoes1 = shoes.FirstOrDefault(c => c.Id == item.IdShoeDetail);
                model.NameShoe = shoes1.Name;
                lstmodel.Add(model);
            }
            return View(lstmodel);
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
                return RedirectToAction("Show");
            }
            return View();
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
                return RedirectToAction("Show");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete3(Guid Id)
        {
            string apiURL = $"https://localhost:7109/api/FavouriteShoes/{Id}";
            var response = await _httpClient.DeleteAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
