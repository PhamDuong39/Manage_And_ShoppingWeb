using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        public IAllRepositories<Coupons> _irepos;
        AppDbContext DbContext;

        public CouponsController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<Coupons> repos = new AllRepositories1<Coupons>(DbContext, DbContext.Coupons);
            _irepos = repos;

        }
        // GET: api/<BillController>
        [HttpGet("Show-Coupons")]
        public IEnumerable<Coupons> GetAllCoupons()
        {
            return _irepos.GetAll();
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Coupons Get(Guid id)
        {
            return _irepos.GetAll().First(p => p.Id == id);
        }
        // POST api/<BillController>
        [HttpPost("Create-Coupons")]
        public bool CreateCoupons(int DiscountValue, int Quantity, string VoucherName)
        {
            
           
            Coupons coupon = new Coupons();
            coupon.DiscountValue = DiscountValue;
            coupon.Quantity = Quantity;
            coupon.VoucherName = VoucherName;
            coupon.TimeStart = DateTime.Now;
            coupon.TimeEnd = DateTime.Now.AddDays(7);
            string formattedStartDate = coupon.TimeStart.ToString("yyyy-MM-ddTHH:mm:ss");
            string formattedEndDate = coupon.TimeEnd.ToString("yyyy-MM-ddTHH:mm:ss");

            // Kiểm tra hạn sử dụng phải lớn hơn 6 giờ
            //DateTime timeEnd = DateTime.Now.AddHours(6);
            //if (coupon.TimeStart > timeEnd)
            //{
            //    coupon.TimeEnd = coupon.TimeStart.AddDays(7);
            //}
            //else
            //{
            //    coupon.TimeEnd = timeEnd.AddDays(7);
            //}
            return _irepos.Create(coupon);
        }
        

        // PUT api/<BillController>/5
        [HttpPut("edit-Coupons-{id}")]
        public bool UpdateCoupons(Guid id, int DiscountValue, int Quantity, string VoucherName, DateTime TimeEnd)
        {
            Coupons coupon = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
            coupon.DiscountValue = DiscountValue;
            coupon.Quantity = Quantity;
            coupon.VoucherName = VoucherName;
            coupon.TimeEnd = TimeEnd;
            return _irepos.Update(coupon);
        }

        // DELETE api/<BillController>/5
        [HttpDelete("Delete-Coupons-/{id}")]
        public bool Delete(Guid id)
        {
            Coupons coupon = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
            return _irepos.Delete(coupon);
        }
    }
}