using Data.Models;

namespace ProjectViews.Areas.User.Models;

public class ShoeViewModel
{
    public List<ShoeCategory> ListShoeCategory { get; set; }
    public ShoeDetails ShoeDetails { get; set; }
}