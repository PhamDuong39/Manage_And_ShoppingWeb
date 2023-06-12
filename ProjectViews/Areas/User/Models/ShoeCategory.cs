namespace ProjectViews.Areas.User.Models;

public class ShoeCategory
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Supplier { get; set; }
  public string Brand { get; set; }
  public string Category { get; set; }
  public int CostPrice { get; set; }
  public int SellPrice { get; set; }
  public int Status { get; set; }
  public int AvailableQuantity { get; set; }
  public int DiscountValue { get; set; }
  public string ColorValue { get; set; }
  public float SizeValue { get; set; }
  public string ImageSource { get; set; }
  public string Decriptions1 { get; set; }
  public string Decriptions2 { get; set; }
  public string Decriptions3 { get; set; }
  public List<string> LstFeedbacks { get; set; }
  public List<int> RateStar { get; set; }
}
