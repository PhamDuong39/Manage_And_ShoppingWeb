using Data.Models;
namespace ProjectViews.Models
{
    public class BillsViewModel
    {
        public Bills bill { get; set; }
        public List<BillDetails> lstBillDT { get; set; }
        //private int _sumPrice;

        public double sumPrice
        {
            get; set;
            //get
            //{
            //    _sumPrice = lstBillDT.Sum(bill => bill.Quantity * bill.Price);
            //    return _sumPrice;
            //}
            //set
            //{
            //    _sumPrice = value;
            //}
        }

        public int deliveryFee { get; set; }
        public double DiscountMoney { get; set; }

        public double NoDiscountPrice { get; set;}
    }
}