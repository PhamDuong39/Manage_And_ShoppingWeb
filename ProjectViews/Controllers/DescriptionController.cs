using System.Text;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Controllers
{
    public class DescriptionController : Controller
    {
       private readonly HttpClient _httpClient;

        public DescriptionController()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IActionResult> Show()
        {
            string apiUrl = $"https://localhost:7109/api/Descriptions/get-all";
            var response = await _httpClient.GetAsync(apiUrl);
            string apidata = await response.Content.ReadAsStringAsync();
            var des = JsonConvert.DeserializeObject<IEnumerable<Descriptions>>(apidata);
            return View(des);
        }
        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Descriptions des)
        {
            string apiUrl =
                $"https://localhost:7109/api/Descriptions/create-description?idShoesDetails={des.IdShoeDetail}&note1={des.Note1}&note2={des.Note2}&note3={des.Note3}";
            var content = new StringContent(JsonConvert.SerializeObject(des), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }
        //Update
        [HttpGet]
        public IActionResult Update(Guid iD)
        {
            string url = $"https://localhost:7109/api/Descriptions/get-by-id?id={iD}";
            var response = _httpClient.GetAsync(url);
            string apiData = response.Result.Content.ReadAsStringAsync().Result;
            var des = JsonConvert.DeserializeObject<Descriptions>(apiData);
            return View(des);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Descriptions des)
        {
            string url = $"https://localhost:7109/api/Descriptions/update-description?id={des.Id}&note1={des.Note1}&note2={des.Note2}&note3={des.Note3}&idShoesDetail={des.IdShoeDetail}";
            var json = JsonConvert.SerializeObject(des);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return View();
        }
        //details
        public IActionResult Detail(Guid Id)
        {
            string url = $"https://localhost:7109/api/Descriptions/get-by-id?id={Id}";
            var response = _httpClient.GetAsync(url);
            string apiData = response.Result.Content.ReadAsStringAsync().Result;
            var des = JsonConvert.DeserializeObject<Descriptions>(apiData);
            return View(des);
        }
        //delete
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/Descriptions/delete-description?id={id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return BadRequest();
        }
    }
}
