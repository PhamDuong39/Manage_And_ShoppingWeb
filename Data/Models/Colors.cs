using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Colors
  {
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string ColorName { get; set; }
    public List<Color_ShoeDetails> Color_ShoeDetails { get; set; }
  }
}
