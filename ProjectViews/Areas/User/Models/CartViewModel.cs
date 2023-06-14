using Data.Models;

namespace ProjectViews.Areas.User.Models
{
	public class CartViewModel
	{
		public Guid IdCartDetail { get; set; }
		public Guid IdShoe { get; set; }	
		public string ShoeName { get; set; }
		public float sizeNumber { get; set; }
		 
		public double price { get; set; }
		public int quantity { get; set; }
		public double SumPriceOfOne { get; set; }
		public string ImageSource { get; set; }
	}
}
