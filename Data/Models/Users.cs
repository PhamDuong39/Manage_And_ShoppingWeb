using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Users
  {
    public Guid Id { get; set; }
    public Guid IdRole { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
    public string Username { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
    public string Password { get; set; }
    public string Address { get; set; }
    //number phone viet nam +84 or 0
    [Required]
    [MaxLength(12, ErrorMessage = "Phone number cannot be longer than 12 characters.")]
    [MinLength(10, ErrorMessage = "Phone number cannot be shorter than 10 characters.")]
    [RegularExpression(@"^((\+84)|0)+([0-9]{9})$", ErrorMessage = "Invalid Phone Number.")]
    public string Phonenumber { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }
    [Required]
    //status: 0: chưa xử lý, 1: đã xử lý, 2: đã giao hàng, 3: đã hủy
    [Range(0, 3, ErrorMessage = "Status must be between 0 and 3.")]
    public int Status { get; set; }
    [Required]
    //max length 100
    [MaxLength(100, ErrorMessage = "Full Name cannot be longer than 100 characters.")]
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
