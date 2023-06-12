using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ShoeDetails
    {
        public Guid Id { get; set; }
        public Guid IdSupplier { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdBrand { get; set; }
        public string Name { get; set; }
        public int CostPrice { get; set; }
        public int SellPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public Guid IdSale { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual Brands Brands { get; set; }
        public virtual Sales Sales { get; set; }
        public List<Descriptions> Descriptions { get; set; }
        public List<Color_ShoeDetails> Color_ShoeDetails { get; set; }
        public List<Images> Images { get; set; }
        public List<Sizes_ShoeDetails> Sizes_ShoeDetails { get; set; }
        public List<BillDetails> BillDetails { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
        public List<CartDetails> CartDetails { get; set; }


        public List<FavouriteShoes> FavoriteShoes { get; set; }
    }
}
