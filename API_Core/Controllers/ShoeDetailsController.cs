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

        private readonly IAllRepositories<Colors> _color;

        private readonly IAllRepositories<Color_ShoeDetails> _colorShoeDetails;

        private readonly IAllRepositories<Sizes> _size;

        private readonly IAllRepositories<Sizes_ShoeDetails> _sizeShoeDetails;
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

        //get by id color and id size
        [HttpGet("get-shoeDetails-by-Color-Size")]
        public ShoeDetails GetShoeByColorAndSize(string colorName, float sizeNumber)
        {
            // Get the color and size entities based on the provided values
            var selectedColor =
                _context.Colors.FirstOrDefault(c => c.ColorName.ToLower().Trim() == colorName.ToLower().Trim());
            var selectedSize = _context.Sizes.FirstOrDefault(s => s.SizeNumber == sizeNumber);

            if (selectedColor == null || selectedSize == null)
            {
                return null; // Return null if the color or size is not found
            }
            // Find the matching shoe details that have both the selected color and size
            var matchingShoeDetails = _context.ShoeDetails.FirstOrDefault(sd =>
                sd.Color_ShoeDetails.Any(csd => csd.IdColor == selectedColor.Id) &&
                sd.Sizes_ShoeDetails.Any(ssd => ssd.IdSize == selectedSize.Id));
            return matchingShoeDetails;
        }



        //update quanity by id
        [HttpPut("update-quantity-by-id")]
        public string UpdateQuantity(Guid id, int quantity)
        {
            var shoeDetails = this._iShoesDetails.GetAll().FirstOrDefault(p => p.Id == id);
            shoeDetails.AvailableQuantity = quantity;
            this._iShoesDetails.Update(shoeDetails);
            return "Update quantity success";
        }


    }
}