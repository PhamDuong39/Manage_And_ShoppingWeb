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
    public class DescriptionsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        private readonly IAllRepositories<Descriptions> _iDesRepos;

        public DescriptionsController()
        {
            var _desRepos = new AllRepositories1<Descriptions>(this._context, this._context.Descriptions);
            this._iDesRepos = _desRepos;
        }

        // create 
        [HttpPost("create-description")]
        public async Task<bool> CreateDescriptionAsync(Guid idShoesDetails, string note1, string note2, string note3)
        {
            //if (this._iDesRepos.GetAll().Any(p => p.IdShoeDetail == idShoesDetails))
            //{
            //    return false;
            //}
            var des = new Descriptions();
            des.Id = Guid.NewGuid();
            des.Note1 = note1;
            des.Note2 = note2;
            des.Note3 = note3;
            des.IdShoeDetail = idShoesDetails;
            return this._iDesRepos.Create(des); // tạo description mới
        }

        // delete
        [HttpDelete("delete-description")]
        public bool DeleteDescription(Guid id)
        {
            var des = this._iDesRepos.GetAll().FirstOrDefault(p => p.Id == id);
            if (this._iDesRepos.Delete(des))
            {
                Console.WriteLine("Delete Done!");
                return true;
            }

            Console.WriteLine("Delete Failed!");
            return false;
        }

        [HttpDelete("delete-many-descriptions")]
        public bool DeleteManyDescriptions(List<Guid> ids)
        {
            try
            {
                var descriptions = new List<Descriptions>();
                foreach (var id in ids)
                {
                    var des = this._iDesRepos.GetAll().FirstOrDefault(p => p.Id == id);
                    descriptions.Add(des);
                }

                if (this._iDesRepos.DeleteMany(descriptions))
                {
                    Console.WriteLine("Delete Done!");
                    return true;
                }

                Console.WriteLine("Delete Failed!");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        // get
        [HttpGet("get-all")]
        public IEnumerable<Descriptions> GetAllDescriptions()
        {
            return this._iDesRepos.GetAll();
        }

        [HttpGet("get-by-id")]
        public Descriptions GetDescriptionById(Guid id)
        {
            return this._iDesRepos.GetAll().FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("get-by-id-shoes-details")]
        public IEnumerable<Descriptions> GetDescriptionsByIdShoesDetails(Guid id)
        {
            return this._iDesRepos.GetAll().Where(p => p.IdShoeDetail == id).ToList();
        }

        // update
        [HttpPut("update-description")]
        public bool UpdateDescription(Guid id, string note1, string note2, string note3 ,Guid idShoesDetail)
        {
            
            var des = this._iDesRepos.GetAll().FirstOrDefault(p => p.Id == id);
            des.Note1 = note1;
            des.Note2 = note2;
            des.Note3 = note3;
            des.IdShoeDetail = idShoesDetail;
            return this._iDesRepos.Update(des);
        }
    }
}