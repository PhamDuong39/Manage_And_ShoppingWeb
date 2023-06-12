using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        public IAllRepositories<CartDetails> _irepos;
        public IAllRepositories<ShoeDetails> _ishoesrepos;
        AppDbContext DbContext;

        public CartDetailController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<CartDetails> repos = new AllRepositories1<CartDetails>(DbContext, DbContext.CartDetails);
            _irepos = repos;

            AllRepositories1<ShoeDetails> shoesrepos = new AllRepositories1<ShoeDetails>(DbContext, DbContext.ShoeDetails);
            _ishoesrepos = shoesrepos;
        }
        // API Controller
      

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<CartDetails> Get()
        {
            return _irepos.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public CartDetails Get(Guid id)
        {
            return _irepos.GetAll().First(p => p.Id == id);
        }

        // POST api/<ValuesController>
        [HttpPost("create-cartdetail")]
        public string CreateCartDetail(Guid IdUser,Guid IdShoesDetail,int Quantity)
        {
            if (!_ishoesrepos.GetAll().Any(p => p.Id == IdShoesDetail))
            {
                return "Loại giày không tồn tại";
            }
            else
            {
                // check xem user nay da co gio hang hay chua
                if (_irepos.GetAll().Any(p => p.IdUser == IdUser && p.IdShoeDetail == IdShoesDetail))
                {

                    CartDetails cartdetail = _irepos.GetAll().FirstOrDefault(p => p.IdUser == IdUser && p.IdShoeDetail == IdShoesDetail);
                    cartdetail.Quantity += Quantity;
                    _irepos.Update(cartdetail);
                    return "Thêm thành công";

                }
                CartDetails cd = new CartDetails();
                cd.IdShoeDetail = IdShoesDetail;
                cd.IdUser = IdUser;

                cd.Quantity = Quantity;
                if (_irepos.Create(cd))
                {
                    return "Thêm thành công";
                }
                else
                {
                    return "Them That bai";
                }
            }
           
        }

        // PUT api/<ValuesController>/5
        //[HttpPut]
        //[Route("update-cartdetail")]
        //public string UpdateCartDetail(Guid id, Guid idShoeDetail, Guid idUser, int quantity)
        //{
        //    CartDetails cartdetail = _irepos.GetAll().First(p => p.Id == id);
        //    //check quantity have more than AvailableQuantity
        //    if (quantity > _irepos.GetAll().First(p => p.Id == id).ShoeDetails.AvailableQuantity)
        //    {
        //        return "Quantity is not enough";
        //    }

        //    cartdetail.Quantity = quantity;
        //    cartdetail.IdUser = idUser;
        //    cartdetail.IdShoeDetail = idShoeDetail;
        //    if (_irepos.Update(cartdetail))
        //    {
        //        return "Update success !";
        //    }
        //    else
        //    {
        //        return "Update fail !";
        //    }
        //}
        // PUT api/<ValuesController>/5
        [HttpPut]
        [Route("update-cartdetail")]
        public bool UpdateCartDetail(Guid id, Guid idShoeDetail, Guid idUser, int quantity)
        {
            CartDetails cartdetail = _irepos.GetAll().First(p => p.Id == id);
            cartdetail.Quantity = quantity;
            cartdetail.IdUser = idUser;
            cartdetail.IdShoeDetail = idShoeDetail;
            return _irepos.Update(cartdetail);
        }
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            CartDetails obj = _irepos.GetAll().First(p => p.Id == id);
            return _irepos.Delete(obj);
        }
    }
}
