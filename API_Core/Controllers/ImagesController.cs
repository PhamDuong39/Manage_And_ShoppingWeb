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
    public class ImagesController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: api/<ImagesController>
        private readonly IAllRepositories<Images> _imagesIRepos;

        public ImagesController()
        {
            var imagesRepos = new AllRepositories1<Images>(this._context, this._context.Images);
            this._imagesIRepos = imagesRepos;
        }

        // create
        [HttpPost("create-image")]
        public bool CreateImage(string imageUrl, Guid idShoesDetails)
        {
            if (string.IsNullOrEmpty(imageUrl)) return false;
            if (this._imagesIRepos.GetAll().Any(p => p.ImageSource == imageUrl))
            {
                return false;
            }

            var image = new Images();
            image.Id = Guid.NewGuid();
            image.ImageSource = imageUrl;
            image.IdShoeDetail = idShoesDetails;
            return this._imagesIRepos.Create(image); // tạo image mới
        }

        // delete
        [HttpDelete("delete-image-by-id")]
        public bool DeleteImageById(Guid id)
        {
            var imageDel = this._imagesIRepos.GetAll().FirstOrDefault(p => p.Id == id);
            return this._imagesIRepos.Delete(imageDel);
        }

        [HttpDelete("delete-many-image-by-id")]
        public bool DeleteManyImageById(List<Guid> ids)
        {
            var imagesDel = new List<Images>();
            foreach (var id in ids)
            {
                var image = this._imagesIRepos.GetAll().FirstOrDefault(p => p.Id == id);
                imagesDel.Add(image);
            }

            return this._imagesIRepos.DeleteMany(imagesDel);
        }

        // get
        [HttpGet("get-all-image")]
        public List<Images> GetAllImages()
        {
            return this._imagesIRepos.GetAll().ToList();
        }

        [HttpGet("get-image-by-id")]
        public Images GetImageById(Guid id)
        {
            return this._imagesIRepos.GetItem(id);
        }

        [HttpGet("get-image-by-sourceString")]
        public List<Images> GetImageBySource(string source)
        {
            return this._imagesIRepos.GetAll().Where(p => p.ImageSource == source).ToList();
        }

        // update
        [HttpPut("update-image-by-id")]
        public bool UpdateImage(Guid id, string imageUrl, Guid idshoesDetails)
        {
            var image = this._imagesIRepos.GetAll().FirstOrDefault(p => p.Id == id);
            image.ImageSource = imageUrl;
            image.IdShoeDetail = idshoesDetails;
            return this._imagesIRepos.Update(image);
        }
    }
}