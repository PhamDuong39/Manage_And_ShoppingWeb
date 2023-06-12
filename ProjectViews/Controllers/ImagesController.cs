using System.Reflection;
using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Controllers
{
    public class ImagesController : Controller
    {
        private readonly HttpClient _httpClient;

        public ImagesController()
        {
            _httpClient = new HttpClient();
        }

        // GET: ImageController
        public async Task<IActionResult> Show()
        {
            string apiUrl = $"https://localhost:7109/api/Images/get-all-image";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var images = JsonConvert.DeserializeObject<IEnumerable<Images>>(apiData);
            return View(images);
        }
        //create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Images images, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                images.ImageSource = fileName;
            }
            string url =
                $"https://localhost:7109/api/Images/create-image?imageUrl={images.ImageSource}&idShoesDetails={images.IdShoeDetail}";
            var json = JsonConvert.SerializeObject(images);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");

        }

        //details
        public async Task<IActionResult> Detail(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7109/api/Images/get-image-by-id?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var image = JsonConvert.DeserializeObject<Images>(result);
            if (response.IsSuccessStatusCode)
            {
                return View(image);
            }
            return NotFound();
        }

        //update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            //get supplier by id
            var response = await _httpClient.GetAsync($"https://localhost:7109/api/Images/get-image-by-id?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var image = JsonConvert.DeserializeObject<Images>(result);
            if (response.IsSuccessStatusCode)
            {
                return View(image);
            }

            return NotFound();
        }

        //delete
        [HttpPost]
        public async Task<IActionResult> Update(Images images, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                images.ImageSource = fileName;
            }

            string url =
                $"https://localhost:7109/api/Images/update-image-by-id?id={images.Id}&imageUrl={images.ImageSource}&idShoesDetails={images.IdShoeDetail}";
            var json = JsonConvert.SerializeObject(images);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");

        }
        //delete
        public async Task<IActionResult> Delete(Guid Id)
        {
            string url = $"https://localhost:7109/api/Images/delete-image-by-id?id={Id}";
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return BadRequest();
        }
    }
}