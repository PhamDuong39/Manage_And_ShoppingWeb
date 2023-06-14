using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Sizes
  {
    public Guid Id { get; set; }
    [Required]
    //size number max 50, min 10
    [Range(10, 50, ErrorMessage = "Size number must be between 10 and 50.")]
    public float SizeNumber { get; set; }
    public List<Sizes_ShoeDetails> Sizes_ShoeDetails { get; set; }
  }
}
