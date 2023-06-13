using Data.Models;

namespace ProjectViews.Areas.User.Models
{
	public class PaymentViewModel
	{
		List<CartDetails> cartDetails { get; set; }
		List<PaymentMethods> paymentMethods { get; set; }
		public Users User { get; set; }
		

		public double TamTinh { get;set; }
		public double Shipping { get; set; }
		public double GiamGiaVoucher { get; set; }
		public double TongTienPhaiTra { get; set; }
	}
}
