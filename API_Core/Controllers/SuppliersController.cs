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
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        private readonly IAllRepositories<Supplier> _suppliersIRepos;

        public SuppliersController()
        {
            var _suppliersRepos = new AllRepositories1<Supplier>(this._context, this._context.Suppliers);
            this._suppliersIRepos = _suppliersRepos;
        }

        // create
        [HttpPost("create-supplier")]
        public bool CreateSupplier(string addressSupplier)
        {
            if (string.IsNullOrEmpty(addressSupplier)) return false;

            // Check if brandName already exists
            if (this._suppliersIRepos.GetAll().Any(p => p.Address == addressSupplier))
            {
                return false;
            }

            var supplier = new Supplier();
            supplier.Id = Guid.NewGuid();
            supplier.Address = addressSupplier;
            return this._suppliersIRepos.Create(supplier); // Create a new brand
        }

        [HttpDelete("delete-many-supplier")]
        public bool DeleteManySupplier(List<Guid> ids)
        {
            var _lstSupplier = this._suppliersIRepos.GetAll().Where(p => ids.Contains(p.Id)).ToList();
            return this._suppliersIRepos.DeleteMany(_lstSupplier);
        }

        // Delete
        [HttpDelete("delete-supplier")]
        public bool DeleteSupplier(Guid id)
        {
            var supplier = this._suppliersIRepos.GetAll().FirstOrDefault(p => p.Id == id);
            if (supplier == null)
            {
                Console.WriteLine("Supplier is not existed");
            }
            else
            {
                Console.WriteLine("Delete Done!");
                return this._suppliersIRepos.Delete(supplier);
            }

            return false;
        }

        // Get
        [HttpGet("get-all-supplier")]
        public IEnumerable<Supplier> GetAllSupplier()
        {
            return this._suppliersIRepos.GetAll();
        }

        [HttpGet("get-supplier-by-address")]
        public List<Supplier> GetSupplierByAddress(string address)
        {
            return this._suppliersIRepos.GetAll().Where(p => p.Address.Contains(address)).ToList();
        }

        [HttpGet("get-supplier-by-id")]
        public Supplier GetSupplierById(Guid id)
        {
            return this._suppliersIRepos.GetItem(id);
        }

        // Update
        [HttpPut("update-supplier")]
        public bool UpdateSupplier(Guid id, string address)
        {
            var supplier = this._suppliersIRepos.GetAll().FirstOrDefault(p => p.Id == id);
            supplier.Address = address;
            return this._suppliersIRepos.Update(supplier);
        }
    }
}