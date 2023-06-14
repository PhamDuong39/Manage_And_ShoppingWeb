using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class ShipAdressMethod
  {
    public Guid Id { get; set; }
    public string NameAddress { get; set; }
    [Required]
    //status: 0: chưa xử lý, 1: đã xử lý, 2: đã giao hàng, 3: đã hủy
    [Range(0, 3, ErrorMessage = "Status must be between 0 and 3.")]
    public int Status { get; set; }
    [Required]
    //price > 0
    [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public int Price { get; set; }
    public List<Bills> Bills { get; set; }
  }
}
