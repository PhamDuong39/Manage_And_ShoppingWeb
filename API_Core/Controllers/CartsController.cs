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
    public class CartsController : ControllerBase
    {
        public IAllRepositories<Carts> _irepos;
        AppDbContext DbContext;

        public CartsController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<Carts> repos = new AllRepositories1<Carts>(DbContext, DbContext.Carts);
            _irepos = repos;

        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Carts> Get()
        {
            return _irepos.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Carts Get(Guid id)
        {
            return _irepos.GetAll().First(p => p.IdUser == id);
        }

        // POST api/<ValuesController>
        [HttpPost("create-carts")]
        public bool CreateCarts(Guid id)
        {
            Carts carts = new Carts();
            carts.IdUser = id;
            return _irepos.Create(carts);
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [Route("update-carts")]
        public bool UpdateCarts(Guid id)
        {
            Carts obj = _irepos.GetAll().First(p => p.IdUser == id);
            return _irepos.Update(obj);
        }

        //delete api/<valuescontroller>/5
        [HttpDelete("delete")]
        public bool delete(Guid id)
        {
            Carts obj = _irepos.GetAll().First(p => p.IdUser == id);
            return _irepos.Delete(obj);
        }
        //Test
    }
}
