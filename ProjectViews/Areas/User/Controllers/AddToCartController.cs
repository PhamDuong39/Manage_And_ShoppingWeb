using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProjectViews.Areas.User.Controllers;
[Area("User")]
public class AddToCartController : Controller
{
    // GET
    HttpClient _httpClient;

    public AddToCartController()
    {
        _httpClient = new HttpClient();
    }
    //add product to cart string color and float size
    [HttpPost]
    public async Task<IActionResult> AddProductToCart(string color , float size, int quantity)
    {
        //remove  # in first in color
        color = color.Remove(0, 1);
        string apiUrl = $"https://localhost:7109/api/ShoeDetails/get-shoeDetails-by-Color-Size?colorName={color}&sizeNumber={size}";
        var response = await _httpClient.GetAsync(apiUrl);
        var apiData = await response.Content.ReadAsStringAsync();
        var shoe = JsonConvert.DeserializeObject<ShoeDetails>(apiData);
        //get user name from session
        var userName = HttpContext.Session.GetString("User");
        if (userName == null)
        {
            return RedirectToAction("LogOutUser","Account");
        }
        else
        {
            //get all user from api
            string apiUrlUser = "https://localhost:7109/api/User/get-all-user";
            var responseUser = await _httpClient.GetAsync(apiUrlUser);
            string apidataUser = await responseUser.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Users>>(apidataUser);
            //get user id
            var user = users.FirstOrDefault(p => p.Username == userName);
            //add product to cart with id user and id product
            string apiUrlCart = $"https://localhost:7109/api/CartDetail/create-cartdetail?IdUser={user.Id}&IdShoesDetail={shoe.Id}&Quantity={quantity}";
            var responseCart = await _httpClient.PostAsync(apiUrlCart, null);
            if (responseCart.IsSuccessStatusCode)
            {
                //thay bang code in view cart
                return Content("Add thanh cong");
            }
            return BadRequest();

        }
    }
}