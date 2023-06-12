using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Color_ShoeDetails
    {
        public Guid Id { get; set; }
        public Guid IdColor { get; set; }
        public Guid IdShoeDetail { get; set; }
        public virtual Colors Colors { get; set; }
        public virtual ShoeDetails ShoeDetails { get; set; }
    }
}
