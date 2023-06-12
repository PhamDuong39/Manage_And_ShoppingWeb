using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {

        public IAllRepositories<PaymentMethods> _irepos;
        AppDbContext DbContext;

        public PaymentMethodController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<PaymentMethods> repos = new AllRepositories1<PaymentMethods>(DbContext, DbContext.PaymentMethods);
            _irepos = repos;

        }
        // GET: api/<PaymentMethodController>
        [HttpGet]
        public IEnumerable<PaymentMethods> GetAllPaymentMethod()
        {
            return _irepos.GetAll();
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("{id}")]
        public PaymentMethods Getone(Guid id)
        {
            return _irepos.GetAll().FirstOrDefault(p => p.Id == id);
        }

        // POST api/<PaymentMethodController>
        [HttpPost("CreatePaymentMethod")]
        public bool CreatePaymentMethod(int status, string Name)
        {
            PaymentMethods payment = new PaymentMethods();
            payment.NameMethod = Name;
            payment.Status = status;
            return _irepos.Create(payment);
        }

        // PUT api/<PaymentMethodController>/5
        [HttpPut("UpdatePaymentMethod")]
        public bool UpdatePaymentMethod(Guid Id, int status, string Name)
        {
            var obj = _irepos.GetAll().FirstOrDefault(p => p.Id == Id);
            obj.NameMethod = Name;
            obj.Status = status;
            return _irepos.Update(obj);
        }

        // DELETE api/<PaymentMethodController>/5
        [HttpDelete("Delete")]
        public bool Delete(Guid id)
        {
            var obj = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
            return _irepos.Delete(obj);
        }
    }
}
