using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Roles
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public List<Users> Users { get; set; }
    }
}
