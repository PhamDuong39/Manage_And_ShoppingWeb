using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Coupons
  {
    public Guid Id { get; set; }
    //discount value >=0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int DiscountValue { get; set; }
    //quantity of voucher > 0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int Quantity { get; set; }
    //voucher name not null
    [Required]
    public string VoucherName { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public List<Bills> Bills { get; set; }
  }
}
