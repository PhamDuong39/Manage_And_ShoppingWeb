using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public Guid IdRole { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Fullname { get; set; }
        public List<Bills> Bills { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
        //public virtual Carts Carts { get; set; }
        public List<FavouriteShoes> FavoriteShoes { get; set; }
        public virtual Roles Roles { get; set; }
        public List<AchivePoint> AchivePoints { get; set; }
        
        public virtual Carts Carts { get; set; }
    }
}
