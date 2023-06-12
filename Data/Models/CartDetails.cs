using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        
        public Guid IdShoeDetail { get; set; }
        public Guid IdUser { get; set; }
        public int Quantity { get; set; }
        public virtual ShoeDetails ShoeDetails { get; set; }
        public virtual Carts Carts { get; set; }
    }
}
