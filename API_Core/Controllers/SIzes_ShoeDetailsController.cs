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
    public class SIzes_ShoeDetailsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        private readonly IAllRepositories<Sizes_ShoeDetails> _iSizeShoeDetailsRepos;

        public SIzes_ShoeDetailsController()
        {
            var iSizeShoeDetailsRepos = new AllRepositories1<Sizes_ShoeDetails>(
                this._context,
                this._context.Sizes_ShoeDetails);
            this._iSizeShoeDetailsRepos = iSizeShoeDetailsRepos;
        }

        // create
        [HttpPost("create-size-shoe-details")]
        public bool CreateSizeShoeDetails(Guid sizeId, Guid shoeDetailsId)
        {
            var sizeShoeDetails = new Sizes_ShoeDetails();
            sizeShoeDetails.Id = Guid.NewGuid();
            sizeShoeDetails.IdSize = sizeId;
            sizeShoeDetails.IdShoeDetails = shoeDetailsId;
            //neu nhu idshoesdetails da co sizeId thi khong cho tao
            var check = this._iSizeShoeDetailsRepos.GetAll()
                .FirstOrDefault(p => p.IdSize == sizeId && p.IdShoeDetails == shoeDetailsId);
            if (check!= null)
            {
                return false;
            }
            return this._iSizeShoeDetailsRepos.Create(sizeShoeDetails); // tạo size mới
        }

        [HttpDelete("delete-many-size-shoe-details")]
        public bool DeleteManySizeShoeDetails(List<Guid> idSizes)
        {
            var sizeShoeDetails = new List<Sizes_ShoeDetails>();
            for (var i = 0; i < idSizes.Count; i++)
            {
                var sizeShoeDetail = this._iSizeShoeDetailsRepos.GetAll().Where(p => p.IdSize == idSizes[i])
                    .FirstOrDefault();
                sizeShoeDetails.Add(sizeShoeDetail);
            }

            return this._iSizeShoeDetailsRepos.DeleteMany(sizeShoeDetails);
        }

        // delete
        [HttpDelete("delete-size-shoe-details")]
        public bool DeleteSizeShoeDetails(Guid ID)
        {
            return this._iSizeShoeDetailsRepos.Delete(_iSizeShoeDetailsRepos.GetAll().FirstOrDefault(p => p.Id == ID));
        }

        // get
        [HttpGet("get-all-size-shoe-details")]
        public IEnumerable<Sizes_ShoeDetails> GetAllSizeShoeDetails()
        {
            return this._iSizeShoeDetailsRepos.GetAll();
        }

        [HttpGet("get-size-shoe-details-by-id")]
        public Sizes_ShoeDetails GetSizeShoeDetailsById(Guid id)
        {
            return this._iSizeShoeDetailsRepos.GetAll().FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("get-size-shoe-details-by-id-shoes-details")]
        public Sizes_ShoeDetails GetSizeShoeDetailsByIdShoesDetail(Guid idShoeDetails)
        {
            return this._iSizeShoeDetailsRepos.GetAll().FirstOrDefault(p => p.IdShoeDetails == idShoeDetails);
        }

        [HttpGet("get-size-shoe-details-by-id-size")]
        public Sizes_ShoeDetails GetSizeShoeDetailsByIdSize(Guid idSize)
        {
            return this._iSizeShoeDetailsRepos.GetAll().FirstOrDefault(p => p.IdSize == idSize);
        }

        // update
        [HttpPut("update-size-shoe-details")]
        public bool UpdateSizeShoeDetails(Guid id, Guid sizeId, Guid shoeDetailsId)
        {
            var sizeShoeDetails = this._iSizeShoeDetailsRepos.GetAll().Where(p => p.Id == id).FirstOrDefault();
            sizeShoeDetails.IdSize = sizeId;
            sizeShoeDetails.IdShoeDetails = shoeDetailsId;

            // check trung ten size
            if (sizeId == null || shoeDetailsId == null)
            {
                Console.WriteLine("SizeId or ShoeDetailsId is null or empty");
            }
            else
            {
                Console.WriteLine("Update Done!");
                return this._iSizeShoeDetailsRepos.Update(sizeShoeDetails);
            }

            return false;
        }
    }
}