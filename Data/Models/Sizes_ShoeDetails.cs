using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Sizes_ShoeDetails
    {
        public Guid Id { get; set; }
        public Guid IdSize { get; set; }
        public Guid IdShoeDetails { get; set; }
        public virtual Sizes Sizes { get; set; }
        public virtual ShoeDetails ShoeDetails { get; set; }
    }
}
