using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class FavouriteShoes
    {
        public Guid Id { get; set; }
        public Guid IdShoeDetail { get; set; }
        public int Status { get; set; }
        public Guid IdUser { get; set; }
        public virtual ShoeDetails ShoeDetails { get; set; }
        public virtual Users Users { get; set; }
    }
}
