using System.Text;

using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using ProjectViews.Models;
using ProjectViews.Services;
using static ProjectViews.Services.PaginationExtension;

namespace ProjectViews.Controllers
{
    public class ShoesDetailsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ShoesDetailsController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Show(int? page, int? pageSize)
        {
            int defaultPageSize = 5;

            // kiem tra gia tri cua page. Neu page k co gia tri thi pageNumber = 1. Neu co thi pageNumber = page
            int pageNumber = (page ?? 1);

            // Hiuen thi voi gia tri mac dinh cua so phan tu trong moi trang. Hooc co the tuy chon
            int itemsPerPage = pageSize ?? defaultPageSize;

            //Lay List Shoes Detail
            string apiUrls = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            var responses = await _httpClient.GetAsync(apiUrls); // goi api lay data
            string apiDatas = await responses.Content.ReadAsStringAsync(); // doc data tra ve
            var shoeDetails = JsonConvert.DeserializeObject<IEnumerable<ShoeDetails>>(apiDatas); // chuyen data thanh list

            List<ShoeDetails> shoeDetailsList = shoeDetails.ToList();
            PagedResult<ShoeDetails> pagedResult = PaginationExtension.GetPagedData(shoeDetailsList, pageNumber, itemsPerPage);


            // Tinh toan du lieu cho moi trang
            ViewBag.CurrentPage = pagedResult.CurrentPage;
            ViewBag.TotalPages = pagedResult.TotalPages;
            ViewBag.HasPreviousPage = pagedResult.HasPreviousPage;
            ViewBag.HasNextPage = pagedResult.HasNextPage;
            ViewBag.PreviousPage = pagedResult.CurrentPage - 1;
            ViewBag.NextPage = pagedResult.CurrentPage + 1;

            return View(pagedResult.Data);
        }

        //create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShoeDetails shoeDetails, List<Guid> sizesList, List<Guid> colorsList)
        {
            // Check shoeDetails, sizesList, colorsList is null and return BadRequest
            if (shoeDetails == null || sizesList == null || colorsList == null)
            {
                // Print the count of sizes and colors
                return Content($"Size: {sizesList?.Count ?? 0} Color: {colorsList?.Count ?? 0}");
            }
            else
            {
                for (int i = 0; i < sizesList.Count(); i++)
                {
                    for (int j = 0; j < colorsList.Count(); j++)
                    {
                        ShoeDetails newshoes = new ShoeDetails()
                        {
                            Id = Guid.NewGuid(),
                            Name = shoeDetails.Name,
                            CostPrice = shoeDetails.CostPrice,
                            SellPrice = shoeDetails.SellPrice,
                            AvailableQuantity = shoeDetails.AvailableQuantity,
                            Status = shoeDetails.Status,
                            IdSupplier = shoeDetails.IdSupplier,
                            IdCategory = shoeDetails.IdCategory,
                            IdBrand = shoeDetails.IdBrand,
                            IdSale = shoeDetails.IdSale
                        };
                        string urlApi =
                            $"https://localhost:7109/api/ShoeDetails/create-shoeDetails?IDShoeDetails={newshoes.Id}&name={newshoes.Name}&costPrice={newshoes.CostPrice}&sellPrice={newshoes.SellPrice}&availableQuantity={newshoes.AvailableQuantity}&status={newshoes.Status}&idSupplier={newshoes.IdSupplier}&idCategory={newshoes.IdCategory}&idBrand={newshoes.IdBrand}&idSale={newshoes.IdSale}";
                        var response = await _httpClient.PostAsync(urlApi, null);
                        string urlApiSize =
                            $"https://localhost:7109/api/SIzes_ShoeDetails/create-size-shoe-details?sizeId={sizesList[i]}&shoeDetailsId={newshoes.Id}";
                        var responseSize = await _httpClient.PostAsync(urlApiSize, null);
                        string urlApiColor =
                            $"https://localhost:7109/api/Color_ShoeDetails/create-color-shoeDetails?idShoeDetails={newshoes.Id}&idColor={colorsList[j]}";
                        var responseColor = await _httpClient.PostAsync(urlApiColor, null);
                    }
                }
            }

            // Check shoeDetails, sizesList, colorsList is null and return BadRequest
            return RedirectToAction("Show");
        }

        //edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response =
                await _httpClient.GetAsync($"https://localhost:7109/api/ShoeDetails/get-shoeDetails-by-id?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var shoeDetails = JsonConvert.DeserializeObject<ShoeDetails>(result);
            if (response.IsSuccessStatusCode)
            {
                return View(shoeDetails);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShoeDetails shoeDetails, Guid idSize, Guid idColor)
        {
            // var responseCheck =
            //     await _httpClient.GetAsync(
            //         $"https://localhost:7109/api/ShoeDetails/get-shoeDetails-by-name?name={shoeDetails.Name}");
            // //check neu san pham cung ten da ton tai size va color nay thi khong cho update
            // var resultCheck = await responseCheck.Content.ReadAsStringAsync();
            // var shoeDetailsCheck = JsonConvert.DeserializeObject<ShoeDetails>(resultCheck);
            // if (shoeDetailsCheck != null)
            // {
            //     var responseCheckSize =
            //         await _httpClient.GetAsync(
            //             $"https://localhost:7109/api/Color_ShoeDetails/get-color-shoesDetails-by-id?id={idColor}");
            //     var resultCheckSize = await responseCheckSize.Content.ReadAsStringAsync();
            //     var sizeCheck = JsonConvert.DeserializeObject<Color_ShoeDetails>(resultCheckSize);
            //     var responseCheckColor =
            //         await _httpClient.GetAsync(
            //             $"https://localhost:7109/api/SIzes_ShoeDetails/get-size-shoe-details-by-id?id={idSize}");
            //     var resultCheckColor = await responseCheckColor.Content.ReadAsStringAsync();
            //     var colorCheck = JsonConvert.DeserializeObject<Sizes_ShoeDetails>(resultCheckColor);
            //     if (sizeCheck != null && colorCheck != null)
            //     {
            //         return Content("San pham da ton tai");
            //     }
            // }
            string urlApi =
                $"https://localhost:7109/api/ShoeDetails/update-shoeDetails?id={shoeDetails.Id}&idSupplier={shoeDetails.IdSupplier}&idCategory={shoeDetails.IdCategory}&idBrand={shoeDetails.IdBrand}&name={shoeDetails.Name}&costPrice={shoeDetails.CostPrice}&sellPrice={shoeDetails.SellPrice}&availableQuantity={shoeDetails.AvailableQuantity}&status={shoeDetails.Status}&idSale={shoeDetails.IdSale}";
            var content = new StringContent(JsonConvert.SerializeObject(shoeDetails), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(urlApi, content);
            //update size_shoeDetails
            // string urlApiSize =
            //     $"https://localhost:7109/api/SIzes_ShoeDetails/update-size-shoe-details?sizeId={idSize}&shoeDetailsId={shoeDetails.Id}";
            // var responseSize = await _httpClient.PutAsync(urlApiSize, null);
            // //update color_shoeDetails
            // string urlApiColor =
            //     $"https://localhost:7109/api/Color_ShoeDetails/update-color-shoesDetails?idShoeDetails={shoeDetails.Id}&idColor={idColor}";
            // var responseColor = await _httpClient.PutAsync(urlApiColor, null);
            if (response.IsSuccessStatusCode  )
            {
                return RedirectToAction("Show");
            }
            return View(shoeDetails);
        }

        //details
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var response =
                await _httpClient.GetAsync($"https://localhost:7109/api/ShoeDetails/get-shoeDetails-by-id?id={id}");
            var result = await response.Content.ReadAsStringAsync();
            var shoeDetails = JsonConvert.DeserializeObject<ShoeDetails>(result);
            if (response.IsSuccessStatusCode)
            {
                return View(shoeDetails);
            }

            return NotFound();
        }

        //delete
        public async Task<IActionResult> Delete(Guid id)
        {
            string apiUrl = $"https://localhost:7109/api/ShoeDetails/delete-shoedetails?id={id}";
            var response = await _httpClient.DeleteAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Show");
            }

            return this.RedirectToAction("Show");
        }

        //delete many
        public async Task<IActionResult> DeleteMany(List<Guid> deleteMany)
        {
            foreach (var item in deleteMany)
            {
                string apiUrl = $"https://localhost:7109/api/ShoeDetails/delete-shoedetails?id={item}";
                var response = await _httpClient.DeleteAsync(apiUrl);
            }

            return RedirectToAction("Show");
        }
    }
}