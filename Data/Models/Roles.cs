using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Roles
  {
    public Guid Id { get; set; }
    [Required]
    [MaxLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string RoleName { get; set; }
    [Required]
    //status 1-3
    [Range(1, 3, ErrorMessage = "Status must be between 1 and 3.")]
    public int Status { get; set; }
    public List<Users> Users { get; set; }
  }
}
