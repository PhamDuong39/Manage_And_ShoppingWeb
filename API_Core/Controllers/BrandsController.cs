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
    public class BrandsController : ControllerBase
    {
        private readonly IAllRepositories<Brands> _brandsIRepos;

        private readonly AppDbContext _context = new AppDbContext();

        public BrandsController()
        {
            var _brandsRepos = new AllRepositories1<Brands>(this._context, this._context.Brands);
            this._brandsIRepos = _brandsRepos;
        }

        [HttpPost("create-brand")]
        public bool CreateBrand(string brandName)
        {
            if (string.IsNullOrEmpty(brandName)) return false;

            // Check if brandName already exists
            if (this._brandsIRepos.GetAll().Any(p => p.Name == brandName))
            {
                return false;

            }
            else
            {
                var brand = new Brands();
                brand.Id = Guid.NewGuid();
                brand.Name = brandName;
                return this._brandsIRepos.Create(brand); // Create a new brand
            }
        }

        // delete
        [HttpDelete("delete-brands-by-id")]
        public bool DeleteBrandById(Guid id)
        {
            var brandDel = this._brandsIRepos.GetAll().FirstOrDefault(i => i.Id == id);
            if (brandDel != null) return this._brandsIRepos.Delete(brandDel);
            return false;
        }

        [HttpDelete("delete-many-brands")]
        public bool DeleteManyBrand(List<Guid> Id)
        {
            try
            {
                var brandLst = new List<Brands>();
                foreach (var item in Id)
                {
                    var brand = this._brandsIRepos.GetAll().FirstOrDefault(p => p.Id == item);
                    brandLst.Add(brand);
                }

                return this._brandsIRepos.DeleteMany(brandLst);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // get
        [HttpGet("get-all-brands")]
        public IEnumerable<Brands> GetAllBrands()
        {
            return this._brandsIRepos.GetAll();
        }

        [HttpGet("get-brand-by-id")]
        public Brands GetBrandById(Guid Id)
        {
            return this._brandsIRepos.GetItem(Id);
        }

        [HttpGet("get-brand-by-name")]
        public List<Brands> GetBrandByName(string name)
        {
            return this._brandsIRepos.GetAll().Where(p => p.Name.Contains(name)).ToList();
        }

        // update
        [HttpPut("update-brand-by-id")]
        public bool UpdateBrand(Guid Id, string brandName)
        {
            var brandUpdate = this._brandsIRepos.GetAll().FirstOrDefault(i => i.Id == Id); // lấy màu có id tương ứng
            brandUpdate.Name = brandName; // cập nhật tên màu
            return this._brandsIRepos.Update(brandUpdate); // cập nhật màu
        }
    }
}