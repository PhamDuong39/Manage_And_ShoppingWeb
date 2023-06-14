using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class AchivePoint
  {
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid IdUser { get; set; }
    [Required]
    public int PointValue { get; set; }
    public virtual Users Users { get; set; }
  }
}
