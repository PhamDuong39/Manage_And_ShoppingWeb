using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AchivePoint
    {
        public Guid Id { get; set; }
        public Guid IdUser { get; set; }
        public int PointValue { get; set; } 
        public virtual Users Users { get; set; } 
    }
}
