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
    public class ShoeDetailsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        private readonly IAllRepositories<ShoeDetails> _iShoesDetails;

        public ShoeDetailsController()
        {
            var _shoesDetails = new AllRepositories1<ShoeDetails>(this._context, this._context.ShoeDetails);
            this._iShoesDetails = _shoesDetails;
        }

        // create
        [HttpPost("create-shoeDetails")]
        public bool CreateShoeDetails(
            Guid IDShoeDetails,
            Guid idSupplier,
            Guid idCategory,
            Guid idBrand,
            string name,
            int costPrice,
            int sellPrice,
            int availableQuantity,
            int status,
            Guid idSale)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var shoeDetails = new ShoeDetails();
            shoeDetails.Id = IDShoeDetails;
            shoeDetails.IdSupplier = idSupplier;
            shoeDetails.IdCategory = idCategory;
            shoeDetails.IdBrand = idBrand;
            shoeDetails.Name = name;
            shoeDetails.CostPrice = costPrice;
            shoeDetails.SellPrice = sellPrice;
            shoeDetails.AvailableQuantity = availableQuantity;
            shoeDetails.Status = status;
            shoeDetails.IdSale = idSale;
            // check name trung nhau
            return this._iShoesDetails.Create(shoeDetails); // tạo shoeDetails mới
        }

        // delete many
        [HttpDelete("delete-many-shoedetails")]
        public bool DeleteManyShoeDetails(List<Guid> idLst)
        {
            var shoeDetailsLst = new List<ShoeDetails>();
            foreach (var id in idLst) shoeDetailsLst.Add(this._iShoesDetails.GetAll().FirstOrDefault(p => p.Id == id));
            return this._iShoesDetails.DeleteMany(shoeDetailsLst);
        }

        // delete
        [HttpDelete("delete-shoedetails")]
        public bool DeleteShoeDetails(Guid id)
        {
            return this._iShoesDetails.Delete(this._iShoesDetails.GetAll().FirstOrDefault(p => p.Id == id));
        }

        [HttpGet("get-all-shoeDetails")]
        public IEnumerable<ShoeDetails> GetAllShoeDetails()
        {
            return this._iShoesDetails.GetAll();
        }

        // get
        [HttpGet("get-shoeDetails-by-id")]
        public ShoeDetails GetShoeDetails(Guid id)
        {
            return this._iShoesDetails.GetAll().FirstOrDefault(p => p.Id == id);
        }

        // get by name
        [HttpGet("get-shoeDetails-by-name")]
        public List<ShoeDetails> GetShoeDetailsByName(string name)
        {
            return this._iShoesDetails.GetAll().Where(p => p.Name == name).ToList();
        }

        // update
        [HttpPut("update-shoeDetails")]
        public bool UpdateShoesDetails(
            Guid id,
            Guid idSupplier,
            Guid idCategory,
            Guid idBrand,
            string name,
            int costPrice,
            int sellPrice,
            int availableQuantity,
            int status,
            Guid idSale)
        {
            if (string.IsNullOrEmpty(name)) return false;
            // Check if brandName already exists
            var shoeDetails = this._iShoesDetails.GetAll().FirstOrDefault(p => p.Id == id);
            shoeDetails.IdSupplier = idSupplier;
            shoeDetails.IdCategory = idCategory;
            shoeDetails.IdBrand = idBrand;
            shoeDetails.Name = name;
            shoeDetails.CostPrice = costPrice;
            shoeDetails.SellPrice = sellPrice;
            shoeDetails.AvailableQuantity = availableQuantity;
            shoeDetails.Status = status;
            shoeDetails.IdSale = idSale;
            return this._iShoesDetails.Update(shoeDetails);
        }
    }
}