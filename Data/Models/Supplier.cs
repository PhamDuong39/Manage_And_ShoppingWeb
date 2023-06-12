using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public List<ShoeDetails> ShoeDetails { get; set;}
    }
}
