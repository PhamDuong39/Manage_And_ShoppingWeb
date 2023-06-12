using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Sizes
    {
        public Guid Id { get; set; }
        public float SizeNumber { get; set; }
        public List<Sizes_ShoeDetails> Sizes_ShoeDetails { get; set; }
    }
}
