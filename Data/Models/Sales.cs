using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Sales
  {
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public int DiscountValue { get; set; }
    [Required]
    public string SaleName { get; set; }
    [Required]
    //start date is time
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }
    [Required]
    //end date is time
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
    public List<ShoeDetails> ShoeDetails { get; set; }
  }
}
