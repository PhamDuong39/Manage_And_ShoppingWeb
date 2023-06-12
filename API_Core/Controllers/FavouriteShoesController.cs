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
    public class FavouriteShoesController : ControllerBase
    {
        public IAllRepositories<FavouriteShoes> _irepos;
        AppDbContext DbContext;

        public FavouriteShoesController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<FavouriteShoes> repos = new AllRepositories1<FavouriteShoes>(DbContext, DbContext.FavouriteShoes);
            _irepos = repos;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<FavouriteShoes> GetAllFavouriteShoes()
        {
            return _irepos.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public FavouriteShoes Get(Guid id)
        {
            return _irepos.GetAll().First(p => p.Id == id);
        }

        // POST api/<ValuesController>
        [HttpPost("create-favouriteshoes")]
        public bool CreateFavouriteShoes(Guid idUser, Guid idShoes, int status)
        {
            FavouriteShoes obj = new FavouriteShoes();
            obj.Id = Guid.NewGuid();
            obj.IdUser = idUser;
            obj.IdShoeDetail = idShoes;
            obj.Status = status;
            return _irepos.Create(obj);
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [Route("edit-favouriteshoes")]
        public bool UpdateFavouriteShoes(Guid id, Guid idUser, Guid idShoesDetail, int status)
        {
            FavouriteShoes obj = new FavouriteShoes();
            obj.Id = id;
            obj.IdUser = idUser;
            obj.IdShoeDetail = idShoesDetail;
            obj.Status = status;
            return _irepos.Update(obj);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            FavouriteShoes obj = _irepos.GetAll().First(p => p.Id == id);
            return _irepos.Delete(obj);
        }
    }
}
