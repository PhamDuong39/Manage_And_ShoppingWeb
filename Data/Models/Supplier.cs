using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Supplier
  {
    public Guid Id { get; set; }
    [Required]
    [MaxLength(1000, ErrorMessage = "Name cannot be longer than 1000 characters.")]
    public string Address { get; set; }
    public List<ShoeDetails> ShoeDetails { get; set; }
  }
}
