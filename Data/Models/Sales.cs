using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Sales
    {
        public Guid Id { get; set; }
        public int DiscountValue { get; set; }
        public string SaleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ShoeDetails> ShoeDetails { get; set; }
    }
}
