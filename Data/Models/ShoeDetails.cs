using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Required]
    public string Name { get; set; }
    [Required]
    //cost price >= 0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int CostPrice { get; set; }
    [Required]
    //sell price >= 0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int SellPrice { get; set; }
    [Required]
    //available quantity >= 0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int AvailableQuantity { get; set; }
    [Required]
    //status 1-3
    [Range(1, 3, ErrorMessage = "Status must be between 1 and 3.")]
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
