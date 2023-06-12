using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Stage { get; set; } // Tỉnh or Thành phố
        public string District { get; set; } // Huyện
        public string Ward { get; set; } // Xã
        public string Street { get; set; } // Đường
        public string Address { get; set; } // Địa chỉ 


        public List<Bills> Bills { get; set; } // 1-n
    }
}
