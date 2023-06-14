using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class BillDetails
  {
    public Guid Id { get; set; }
    public Guid IdShoeDetail { get; set; }
    public Guid IdBill { get; set; }
    //price of shoe > 0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int Price { get; set; }
    //quantity of shoe > 0
    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    public int Quantity { get; set; }
    public virtual ShoeDetails ShoeDetails { get; set; }
    public virtual Bills Bills { get; set; }

  }
}
