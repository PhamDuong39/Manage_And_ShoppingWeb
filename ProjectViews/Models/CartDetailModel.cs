using Data.Models;

namespace ProjectViews.Models
{

    public class CartDetailModel
    {


        public CartDetails cartDetail { get; set; }
        public Guid Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int ToTalPrice { get; set; }


    }
}
