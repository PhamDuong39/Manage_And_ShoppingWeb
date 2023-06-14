using Data.Models;

namespace ProjectViews.Areas.User.Models
{
	public class PaymentViewModel
	{
		public List<CartViewModel> listCartItems { get; set; }
		public List<PaymentMethods> paymentMethods { get; set; }
		public Users User { get; set; }
		
		public List<Coupons> coupons { get; set; }		

		public List<ShipAdressMethod> shipAdressMethods { get; set; }
		public double TamTinh { get;set; }
		public double Shipping { get; set; }
		public double GiamGiaVoucherPercent { get; set; }
		public double TongTienPhaiTra { get; set; }
	}
}
