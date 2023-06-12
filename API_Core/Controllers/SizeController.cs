// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Core.Controllers
{

    using Data.IRepositories;
    using Data.Models;
    using Data.Repositories;
    using Data.ShopContext;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: api/<SizeController>
        private readonly IAllRepositories<Sizes> _sizeIrepos;

        public SizeController()
        {
            var _sizeRepos = new AllRepositories1<Sizes>(this._context, this._context.Sizes);
            this._sizeIrepos = _sizeRepos;
        }

        // create
        [HttpPost("create-size")]
        public bool CreateSize(float sizeNumberCreate)
        {
            var size = new Sizes();
            size.Id = Guid.NewGuid();
            size.SizeNumber = sizeNumberCreate;

            // check trung ten size
            if (float.IsNaN(sizeNumberCreate)) return false;

            // Check if brandName already exists
            if (this._sizeIrepos.GetAll().Any(p => p.SizeNumber == sizeNumberCreate))
            {
                return false;
            }

            Console.WriteLine("Create Done!");
            return this._sizeIrepos.Create(size); // tạo size mới
        }

        [HttpDelete("delete-many-sizes")]
        public bool DeleteManySizes(List<Guid> ids)
        {
            var sizes = this._sizeIrepos.GetAll().Where(i => ids.Contains(i.Id)).ToList();
            return this._sizeIrepos.DeleteMany(sizes);
        }

        // delete
        [HttpDelete("delete-size-by-id")]
        public bool DeleteSizeById(Guid id)
        {
            var size = this._sizeIrepos.GetAll().FirstOrDefault(i => i.Id == id);
            return this._sizeIrepos.Delete(size);
        }

        // get
        [HttpGet("get-all-size")]
        public IEnumerable<Sizes> GetAllSize()
        {
            return this._sizeIrepos.GetAll();
        }

        [HttpGet("get-size-by-id/{id}")]
        public Sizes GetSizeById(Guid id)
        {
            return this._sizeIrepos.GetItem(id);
        }

        [HttpGet("get-size-by-number/{number}")]
        public List<Sizes> GetSizeByNumber(float number)
        {
            return this._sizeIrepos.GetAll().Where(i => i.SizeNumber == number).ToList();
        }

        // update
        [HttpPut("update-size-by-id")]
        public bool UpdateSizeById(Guid id, float sizeNumberUpdate)
        {
            var size = this._sizeIrepos.GetAll().FirstOrDefault(i => i.Id == id);
            size.SizeNumber = sizeNumberUpdate;
            return this._sizeIrepos.Update(size);
        }
    }
}