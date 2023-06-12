using Data.Models;

namespace ProjectViews.Areas.User.Models
{
    public class HomeUserViewModel
    {
        public List<ShoeHomePageViewModel> bestSellers { get; set; }
       
        public List<ShoeHomePageViewModel> bestDiscounts { get; set; }

        public List<ShoeHomePageViewModel> newArrivals { get; set; }
    }
}
