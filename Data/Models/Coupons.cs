using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Coupons
    {
        public Guid Id { get; set; }
        public int DiscountValue { get; set; }
        public int Quantity { get; set; }
        public string VoucherName { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public List<Bills> Bills { get; set; }
    }
}
