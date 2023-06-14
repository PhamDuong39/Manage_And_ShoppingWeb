using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectViews.Areas.User.Models;

namespace ProjectViews.Areas.User.Controllers;

[Area("User")]
public class ShopController : Controller
{
  private readonly HttpClient _httpClient;

  public ShopController()
  {
    _httpClient = new HttpClient();
  }

  public async Task<IActionResult> Index()
  {
    // Get all bill
    var apiUrls = "https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
    var responses = await _httpClient.GetAsync(apiUrls); // goi api lay data
    var apiDatas = await responses.Content.ReadAsStringAsync(); // doc data tra ve
    var shoeDetails = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDatas);
    // Get all Sale Event
    var apiURLss = "https://localhost:7109/api/Sales/Show-Sales";
    var responsess = await _httpClient.GetAsync(apiURLss);
    var apiDatass = await responsess.Content.ReadAsStringAsync();
    var sales = JsonConvert.DeserializeObject<List<Sales>>(apiDatass);
    //lay dữ liệu color từ bảng Color
    var apiUrlcolr = "https://localhost:7109/api/Color/get-all-colors";
    var responsecolr = await _httpClient.GetAsync(apiUrlcolr);
    var apiDatacolr = await responsecolr.Content.ReadAsStringAsync();
    var colors = JsonConvert.DeserializeObject<List<Colors>>(apiDatacolr);
    //lay dữ liệu size từ bảng Size
    var apiUrlsize = "https://localhost:7109/api/Size/get-all-size";
    var responsesize = await _httpClient.GetAsync(apiUrlsize);
    var apiDatasize = await responsesize.Content.ReadAsStringAsync();
    var sizes = JsonConvert.DeserializeObject<List<Sizes>>(apiDatasize);
    //lay du lieu tu bang Brand
    var apiUrlbrand = "https://localhost:7109/api/Brand/get-all-brands";
    var responsebrand = await _httpClient.PostAsync(apiUrlbrand, null);
    var apiDatabrand = await responsebrand.Content.ReadAsStringAsync();
    var brands = JsonConvert.DeserializeObject<List<Brands>>(apiDatabrand);
    //lay du lieu tu bang size_shoeDetail
    var apiUrlsize_shoeDetail = "https://localhost:7109/api/SIzes_ShoeDetails/get-all-size-shoe-details";
    var responsesize_shoeDetail = await _httpClient.GetAsync(apiUrlsize_shoeDetail);
    var apiDatasize_shoeDetail = await responsesize_shoeDetail.Content.ReadAsStringAsync();
    var size_shoeDetails = JsonConvert.DeserializeObject<List<Sizes_ShoeDetails>>(apiDatasize_shoeDetail);
    //lay du lieu tu bang color_shoeDetail
    var apiUrlcolor_shoeDetail = "https://localhost:7109/api/Color_ShoeDetails/get-all-color_shoeDetails";
    var apiDatacolor_shoeDetail = await _httpClient.GetAsync(apiUrlcolor_shoeDetail);
    var apiColorShoesDetails = await apiDatacolor_shoeDetail.Content.ReadAsStringAsync();
    var color_shoeDetails = JsonConvert.DeserializeObject<List<Color_ShoeDetails>>(apiColorShoesDetails);
    //lay du lieu tu bang image_shoeDetail
    string apiUrlImage = $"https://localhost:7109/api/Images/get-all-image";
    var responseImage = await _httpClient.GetAsync(apiUrlImage);
    string apiDataImage = await responseImage.Content.ReadAsStringAsync();
    var image_shoeDetails = JsonConvert.DeserializeObject<List<Images>>(apiDataImage);

    //lay du lieu category
    string apiUrl = $"https://localhost:7109/api/Categories/get-all-categories";
    var response = await _httpClient.GetAsync(apiUrl);
    string apidata = await response.Content.ReadAsStringAsync();
    var cat = JsonConvert.DeserializeObject<List<Categories>>(apidata);
    //lay du lieu tu bang supplier
    string apiUrl1 = "https://localhost:7109/api/Suppliers/get-all-supplier";
    var response1 = await _httpClient.GetAsync(apiUrl1);
    string apidata1 = await response1.Content.ReadAsStringAsync();
    var sp = JsonConvert.DeserializeObject<List<Supplier>>(apidata1);
    //lay du lieu tu bang brand
    string apiUrl2 = "https://localhost:7109/api/Brands/get-all-brands";
    var response2 = await _httpClient.GetAsync(apiUrl2);
    string apidata2 = await response2.Content.ReadAsStringAsync();
    var br = JsonConvert.DeserializeObject<List<Brands>>(apidata2);
    //lay du lieu tu bang descripion
    string apiUrlDescriptionsGetAll = $"https://localhost:7109/api/Descriptions/get-all";
    var responseDescriptionsGetAll = await _httpClient.GetAsync(apiUrlDescriptionsGetAll);
    string apiDataDescriptionsGetAll = await responseDescriptionsGetAll.Content.ReadAsStringAsync();
    var descriptions = JsonConvert.DeserializeObject<List<Descriptions>>(apiDataDescriptionsGetAll);
    //lay du lieu tu bang Feedback
    string apiUrlFeedbackGetAll = $"https://localhost:7109/api/Feedbacks/get-all-feedbacks";
    var responseFeedbackGetAll = await _httpClient.GetAsync(apiUrlFeedbackGetAll);
    string apiDataFeedbackGetAll = await responseFeedbackGetAll.Content.ReadAsStringAsync();
    var feedbacks = JsonConvert.DeserializeObject<List<Feedbacks>>(apiDataFeedbackGetAll);
    //Group shoe by name and add to list
    var lstShoes = new List<ShoeCategory>();
    var gourpList = shoeDetails.GroupBy(p => p.Name).Select(p => p.First()).ToList();
    foreach (var item in gourpList)
    {
      ShoeCategory shoe = new ShoeCategory();
      shoe.Id = item.Id;
      shoe.Name = item.Name;
      // shoe.Supplier = sp.FirstOrDefault(p => p.Id == item.IdSupplier).Address;
      // shoe.Brand = br.FirstOrDefault(p => p.Id == item.IdBrand).Name;
      // shoe.Category = cat.FirstOrDefault(p => p.Id == item.IdCategory).CategoryName;
      // shoe.CostPrice = item.CostPrice;
      shoe.SellPrice = item.SellPrice;
      shoe.Status = item.Status;
      shoe.AvailableQuantity = item.AvailableQuantity;
      shoe.DiscountValue = sales.FirstOrDefault(p => p.Id == item.IdSale).DiscountValue;
      // shoe.ColorValue = "#" + colors.FirstOrDefault(p => p.Id == color_shoeDetails.FirstOrDefault(p => p.IdShoeDetail == item.Id).IdColor).ColorName;
      // shoe.SizeValue = sizes.FirstOrDefault(p => p.Id == size_shoeDetails.FirstOrDefault(p => p.IdShoeDetails == item.Id).IdSize).SizeNumber;
      //check image null
      if (image_shoeDetails.FirstOrDefault(p => p.IdShoeDetail == item.Id) == null)
      {
        shoe.ImageSource="https://www.thermaxglobal.com/wp-content/uploads/2020/05/image-not-found.jpg";
      }
      else
      {
        shoe.ImageSource = image_shoeDetails.FirstOrDefault(p => p.IdShoeDetail == item.Id).ImageSource;
      }
      // if (descriptions.FirstOrDefault(p => p.IdShoeDetail == item.Id) != null)
      // {
      //   shoe.Decriptions1 = descriptions.FirstOrDefault(p => p.IdShoeDetail == item.Id).Note1;
      //   shoe.Decriptions2 = descriptions.FirstOrDefault(p => p.IdShoeDetail == item.Id).Note2;
      //   shoe.Decriptions3 = descriptions.FirstOrDefault(p => p.IdShoeDetail == item.Id).Note3;
      // }
      // else
      // {
      //   shoe.Decriptions1 = "No description";
      //   shoe.Decriptions2 = "No description";
      //   shoe.Decriptions3 = "No description";
      // }
      // //check feedback null
      // if (feedbacks.FirstOrDefault(p => p.IdShoeDetail == item.Id) == null)
      // {
      //   shoe.RateStar = new List<int>();
      //   shoe.RateStar.Add(0);
      //   shoe.LstFeedbacks = new List<string>();
      //   shoe.LstFeedbacks.Add("No feedback");
      // }
      // else
      // {
      //   shoe.LstFeedbacks = feedbacks.Where(p => p.IdShoeDetail == item.Id).Select(p => p.Note).ToList();
      //   shoe.RateStar = feedbacks.Where(p => p.IdShoeDetail == item.Id).Select(p => p.RatingStar).ToList();
      //
      // }
      lstShoes.Add(shoe);
    }
    return View(lstShoes);
  }
}
