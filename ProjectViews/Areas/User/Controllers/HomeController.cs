
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using ProjectViews.Areas.User.Models;
using System.Net.Http;

namespace ProjectViews.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        public HomeController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["user"] = HttpContext.Session.GetString("User");

            // Get all bill
            string apiURL = $"https://localhost:7109/api/BillDetails";
            var response = await _httpClient.GetAsync(apiURL);
            string apiData = await response.Content.ReadAsStringAsync();
            var billDetails = JsonConvert.DeserializeObject<List<BillDetails>>(apiData);
            var lstIdShoeInBillDetail = billDetails.Select(p => p.IdShoeDetail);

            // Get all ShoeDetails
            string apiUrls = $"https://localhost:7109/api/ShoeDetails/get-all-shoeDetails";
            var responses = await _httpClient.GetAsync(apiUrls); // goi api lay data
            string apiDatas = await responses.Content.ReadAsStringAsync(); // doc data tra ve
            var shoeDetails = JsonConvert.DeserializeObject<List<ShoeDetails>>(apiDatas);

            // Get all Sale Event
            string apiURLss = $"https://localhost:7109/api/Sales/Show-Sales";
            var responsess = await _httpClient.GetAsync(apiURLss);
            var apiDatass = await responsess.Content.ReadAsStringAsync();
            var sales = JsonConvert.DeserializeObject<List<Sales>>(apiData);

            HomeUserViewModel homeVMD = new HomeUserViewModel();


			//var ListShoeAfterMerge = shoeDetails.GroupBy(p => p.Name).Select(p => p.First()).ToList();

			var ListShoeAfterMerge = new List<ShoeDetails>();
			// Này để theo dõi các name đã lấy để không bị trùng
			var seenNames = new HashSet<string>();
			// Hashset : 1 collection không chứa các ptu trùng lặp - chỗ này check theo name
			int index = 0;
			while (index < shoeDetails.Count)
			{
				var shoe = shoeDetails[index];
				// nếu name đã có trong seesname thì bỏ qua đối tượng này đén thằng kế típ
				if (!seenNames.Contains(shoe.Name))
				{
					ListShoeAfterMerge.Add(shoe);
					//name chưa có thì add vào seesname. 
					seenNames.Add(shoe.Name);
				}
				index++;
			}



			#region TOP SELLER

			// Merge bill vào trước để lọc được các bill có cùng IDShoeDetail => Cộng xem cái IdShoeDetail nào đc
			//Mua nhiều nhất
			var mergedBillDetails = billDetails.GroupBy(p => p.IdShoeDetail).Select(p => new BillDetails
			{
				IdShoeDetail = p.Key,
				Quantity = p.Sum(p => p.Quantity)
			}).OrderByDescending(p => p.Quantity);

			//Bên trên được cái list chứa các Bill trùng IdShoeDetail mua nhiefu nhất r
			// Đến chỗ này thì check Id này sang bảng ShoeDetail Để lấy được List ShoeVMD có trùng tên(1 Name
			// có thẻ có nhiều IdShoeDetail - do nhiều size, color)
			List<ShoeHomePageViewModel> LSTsHOEvmd = new List<ShoeHomePageViewModel>();
			foreach (var item in mergedBillDetails)
			{
				var shoeDetail = shoeDetails.FirstOrDefault(p => p.Id == item.IdShoeDetail);
				ShoeHomePageViewModel shoeVMD = new ShoeHomePageViewModel();
				shoeVMD.Name = shoeDetail.Name;
				shoeVMD.Price = shoeDetail.SellPrice;
				shoeVMD.Quantity = item.Quantity;
				LSTsHOEvmd.Add(shoeVMD);
			}

			if (ListShoeAfterMerge.Count < 8)
			{
				// Có List sản phẩm trùng tên rồi. Đến chỗ này cái nào trùng thì merge nó lại, cộng dồn số lượng
				// Lấy 4 cái SL nhiều nhất
				var lstAfterMergeAndSort = LSTsHOEvmd.GroupBy(p => p.Name).Select(p => new ShoeHomePageViewModel
				{
					Name = p.Key,
					Quantity = p.Sum(c => c.Quantity),
					Price = p.First().Price,
				}).OrderByDescending(p => p.Quantity).Take(ListShoeAfterMerge.Count).ToList();
				homeVMD.bestSellers = lstAfterMergeAndSort;
			}
			else
			{
				// Có List sản phẩm trùng tên rồi. Đến chỗ này cái nào trùng thì merge nó lại, cộng dồn số lượng
				// Lấy 4 cái SL nhiều nhất
				var lstAfterMergeAndSort = LSTsHOEvmd.GroupBy(p => p.Name).Select(p => new ShoeHomePageViewModel
				{
					Name = p.Key,
					Quantity = p.Sum(c => c.Quantity),
					Price = p.First().Price,
				}).OrderByDescending(p => p.Quantity).Take(8).ToList();
				homeVMD.bestSellers = lstAfterMergeAndSort;
			}

			#endregion


			// Get the top 4 shoe in the BillDetail - TOP PRODUCT IN SHOP


			// Get the top 4 newest shoe in the shoeDEtail - NEW ARRIVALS IN SHOP
			#region New Arrivals

			List<ShoeDetails> lstNewArrival = new List<ShoeDetails>();
			if (ListShoeAfterMerge.Count < 8)
			{
				foreach (var item in ListShoeAfterMerge)
				{
					lstNewArrival.Add(item);
				}
			}
			else
			{
				for (int i = ListShoeAfterMerge.Count - 8; i < ListShoeAfterMerge.Count; i++)
				{
					ShoeDetails shoe = ListShoeAfterMerge[i];
					lstNewArrival.Add(shoe);
				}
			}

			List<ShoeHomePageViewModel> lstShoeVMDNewArrival = new List<ShoeHomePageViewModel>();
			foreach (var item in lstNewArrival)
			{
				ShoeHomePageViewModel shoeVMD = new ShoeHomePageViewModel();
				shoeVMD.Name = item.Name;
				shoeVMD.Price = item.SellPrice;
				shoeVMD.Quantity = null;
				lstShoeVMDNewArrival.Add(shoeVMD);
			}

			homeVMD.newArrivals = lstShoeVMDNewArrival;

			#endregion

			#region BestDiscount

			//List<ShoeDetails> bestDiscountsList = new List<ShoeDetails>();
			//         var topValueSale = sales.OrderByDescending(p => p.DiscountValue).Take(2).ToList();
			//         foreach (var item in shoeDetails)
			//         {
			//             foreach (var sale in topValueSale)
			//             {
			//                 if (item.IdSale == sale.Id)
			//                 {
			//			bestDiscountsList.Add(item);

			//		}
			//             }
			//         }

			//List<ShoeHomePageViewModel> lstShoeVMDBestSale = new List<ShoeHomePageViewModel>();
			//var groupListBestSale = bestDiscountsList.GroupBy(p => p.Name).Select(p => p.First()).ToList();
			//foreach (var item in groupListBestSale)
			//{
			//	ShoeHomePageViewModel shoeVMD = new ShoeHomePageViewModel();
			//	shoeVMD.Name = item.Name;
			//	shoeVMD.Price = item.SellPrice;
			//	lstShoeVMDBestSale.Add(shoeVMD);
			//}

			//homeVMD.bestDiscounts = lstShoeVMDBestSale;


			#endregion

			return View(homeVMD);
        }
    }
}
