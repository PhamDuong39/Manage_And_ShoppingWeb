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
    public class Color_ShoeDetailsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        private readonly IAllRepositories<Color_ShoeDetails> _iColor_ShoeDtails;

        public Color_ShoeDetailsController()
        {
            var _colorShoeDtails = new AllRepositories1<Color_ShoeDetails>(
                this._context,
                this._context.Color_ShoeDetails);
            this._iColor_ShoeDtails = _colorShoeDtails;
        }

        // create
        [HttpPost("create-color-shoeDetails")]
        public bool CreateColor_ShoeDetails(Guid idShoeDetails, Guid idColor)
        {

            var color_shoeDetails = new Color_ShoeDetails();
            color_shoeDetails.Id = Guid.NewGuid();
            color_shoeDetails.IdShoeDetail = idShoeDetails;
            color_shoeDetails.IdColor = idColor;
            //neu nhu idshoesdetails da co idcolor thi khong cho tao
            var check = this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.IdShoeDetail == idShoeDetails && p.IdColor == idColor);
            if (check != null)
            {
                return false;
            }
            return this._iColor_ShoeDtails.Create(color_shoeDetails); // tạo color_shoeDetails mới
        }

        // delete
        [HttpDelete("delete-color-shoeDetails")]
        public bool DeleteColor_ShoeDetails(Guid id)
        {
            return this._iColor_ShoeDtails.Delete(this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.Id == id));
        }

        [HttpDelete("delete-many-color-shoeDetails")]
        public bool DeleteManyColor_ShoeDetails(List<Guid> idLst)
        {
            var colorShoeDetailLst = new List<Color_ShoeDetails>();
            foreach (var id in idLst)
                colorShoeDetailLst.Add(this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.Id == id));
            return this._iColor_ShoeDtails.DeleteMany(colorShoeDetailLst);
        }

        // get
        [HttpGet("get-all-color_shoeDetails")]
        public IEnumerable<Color_ShoeDetails> GetAllColor_ShoeDetails()
        {
            return this._iColor_ShoeDtails.GetAll();
        }

        [HttpGet("get-color-shoesDetails-by-id")]
        public Color_ShoeDetails GetColorShoesDetailsByID(Guid id)
        {
            return this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("get-color-shoesDetails-by-idColor")]
        public Color_ShoeDetails GetColorShoesDetailsByIDColor(Guid idColor)
        {
            return this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.IdColor == idColor);
        }

        [HttpGet("get-color-shoesDetails-by-idShoeDetails")]
        public Color_ShoeDetails GetColorShoesDetailsByIDShoeDetails(Guid idShoeDetails)
        {
            return this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.IdShoeDetail == idShoeDetails);
        }

        // update
        [HttpPut("update-color-shoesDetails")]
        public bool UpdateColorShoesDetails(Guid Id, Guid IdColor, Guid IdShoesDetails)
        {
            // Check id null da ton tai hay chua
            var colorShoeDetails = this._iColor_ShoeDtails.GetAll().FirstOrDefault(p => p.Id == Id);
            colorShoeDetails.IdColor = IdColor;
            colorShoeDetails.IdShoeDetail = IdShoesDetails;
            return this._iColor_ShoeDtails.Update(colorShoeDetails);
        }
    }
}