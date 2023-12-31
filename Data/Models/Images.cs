﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Images
  {
    public Guid Id { get; set; }
    public Guid IdShoeDetail { get; set; }
    [Required]
    public string ImageSource { get; set; }
    public virtual ShoeDetails ShoeDetails { get; set; }
  }
}
