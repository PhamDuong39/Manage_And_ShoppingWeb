using Data.IRepositories;
using Data.Repositories;
using Data.Models;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        public IAllRepositories<Location> _irepos;
        AppDbContext DbContext;

        public LocationController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<Location> repos = new AllRepositories1<Location>(DbContext, DbContext.Location);
            _irepos = repos;
            
        }
        // GET: api/<LocationController>
        [HttpGet]
        public IEnumerable<Location> GetAllLocation()
        {
            return  _irepos.GetAll();
        }

        // GET api/<Location>/5
        [HttpGet("{id}")]
        public Location Get(Guid id)
        {
            return _irepos.GetAll().FirstOrDefault(p => p.Id == id);
        }

        // POST api/<LocationController>
        [HttpPost("Createlocation")]
        public bool CreateLocation(string stage, string District, string ward, string street, string Address)
        {
            // string stage, string District, string ward, string street, string Address
            Location lc = new Location();
            lc.Stage = stage;
            lc.District = District;
            lc.Ward = ward;
            lc.Street = street;
            lc.Address = Address;
            return  _irepos.Create(lc);

            //return await _irepos.Create(location);
        }

        [HttpDelete("deleteLocation")]
        public bool DeleteLocation(Guid Id)
        {
            var obj = _irepos.GetAll().FirstOrDefault(p => p.Id == Id);
            return _irepos.Delete(obj);
        }


        [HttpPut("updateLocation")]
        public bool UpdateLocation(Guid Id, string stage, string District, string ward, string street, string Address)
        {
            var obj = _irepos.GetAll().FirstOrDefault(p => p.Id == Id);
            obj.Stage = stage;
            obj.District = District;
            obj.Ward = ward;
            obj.Street = street;
            obj.Address = Address;

            return _irepos.Update(obj);
        }

        
    }
}
