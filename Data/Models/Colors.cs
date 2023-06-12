using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Colors
    {
        public Guid Id { get; set; }



        public string ColorName { get; set; }
        public List<Color_ShoeDetails> Color_ShoeDetails { get; set; }
    }
}
