using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Descriptions
  {
    public Guid Id { get; set; }
    public Guid IdShoeDetail { get; set; }
    [Required]
    public string Note1 { get; set; }
    [Required]
    public string Note2 { get; set; }
    [Required]
    public string Note3 { get; set; }
    public virtual ShoeDetails ShoeDetails { get; set; }
  }
}
