using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Categories
  {
    public Guid Id { get; set; }
    //category name not null
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string CategoryName { get; set; }
    public List<ShoeDetails> ShoeDetails { get; set; }
  }
}
