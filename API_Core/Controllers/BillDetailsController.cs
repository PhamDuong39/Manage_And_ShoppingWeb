using Data.IRepositories;
using Data.Repositories;

using Data.Models;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Core.Controllers
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailsController : ControllerBase
    {
        public IAllRepositories<BillDetails> _irepos;
        public IAllRepositories<ShoeDetails> _ishoesrepos;

        AppDbContext DbContext;

        public BillDetailsController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<BillDetails> repos = new AllRepositories1<BillDetails>(DbContext, DbContext.BillDetails);
            AllRepositories1<ShoeDetails> reposShoes = new AllRepositories1<ShoeDetails>(DbContext, DbContext.ShoeDetails);

            _irepos = repos;
            _ishoesrepos = reposShoes;

        }

        // GET: api/<BillDetailsController>
        [HttpGet]
        public IEnumerable<BillDetails> GetAllBillDetails()
        {
            return _irepos.GetAll();
        }

        // GET api/<BillDetailsController>/5
        [HttpGet("{id}")]
        public BillDetails Getone(Guid id)
        {
            return _irepos.GetAll().FirstOrDefault(p => p.Id == id);
        }

        //POST api/<BillDetailsController>
        [HttpPost]
        public string Post(Guid IdShoeDetail, Guid IdBill, int price, int quantity) // tra ve string ne
        {
            if (!_ishoesrepos.GetAll().Any(p => p.Id == IdShoeDetail))
            {
                return "Loại giày không tồn tại";
            }
            else
            {
                //kiem tra neu hoa don da ton tai va sp da ton tai thi tang quantity
                if (_irepos.GetAll().Any(p => p.IdBill == IdBill && p.IdShoeDetail == IdShoeDetail))
                {

                    BillDetails bds = _irepos.GetAll().FirstOrDefault(p => p.IdBill == IdBill && p.IdShoeDetail == IdShoeDetail);
                    bds.Quantity += quantity;
                    _irepos.Update(bds);
                    return "Thêm thành công";

                }
                BillDetails bd = new BillDetails();
                bd.IdShoeDetail = IdShoeDetail;
                bd.IdBill = IdBill;
                bd.Price = price;
                bd.Quantity = quantity;
                if (_irepos.Create(bd))
                {
                    return "Thêm thành công";
                }
                else
                {
                    return "Them That bai";
                }

            }
        }

        // PUT api/<BillDetailsController>/5
        [HttpPut("{id}")]
        public string UpdateBilldetails(Guid id, Guid IdShoeDetail, Guid IdBill, int price, int quantity)
        {
            //check sp co ton tai khong
            if (!_ishoesrepos.GetAll().Any(p => p.Id == IdShoeDetail))
            {
                return "Loại giày không tồn tại";
            }
            //check so luong co du khong
            else if (_ishoesrepos.GetAll().FirstOrDefault(p => p.Id == IdShoeDetail).AvailableQuantity < quantity)
            {
                return "Số lượng không đủ";
            }
            else
            {
                var obj = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
                obj.IdShoeDetail = IdShoeDetail;
                obj.IdBill = IdBill;
                obj.Price = price;
                obj.Quantity = quantity;
                ShoeDetails sd = _ishoesrepos.GetAll().FirstOrDefault(p => p.Id == IdShoeDetail);
                _irepos.Update(obj);
                //khi update lại số lượng nếu số lương
                return "Sửa thành công";
            }
        }

        // DELETE api/<BillDetailsController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var obj = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
            return _irepos.Delete(obj);

        }

        // GET api/<BillDetailsController>/5
        [HttpGet("FillterByID/{id}")]
        public IEnumerable<BillDetails> FillterByID(Guid id)
        {
            return _irepos.GetAll().Where(p => p.IdBill == id);
        }
    }
}
