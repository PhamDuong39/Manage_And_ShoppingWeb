using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class BillDetails
    {
        public Guid Id { get; set; }
        public Guid IdShoeDetail { get; set; }
        public Guid IdBill { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public virtual ShoeDetails ShoeDetails { get; set;}
        public virtual Bills Bills { get; set; }

    }
}
