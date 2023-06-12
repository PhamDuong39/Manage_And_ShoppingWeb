using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private IAllRepositories<Feedbacks> _feedbacksIrepos;
        private AppDbContext _context = new();
        public FeedbacksController()
        {
            var _feedbacksrepos = new AllRepositories1<Feedbacks>(_context, _context.Feedbacks);
            _feedbacksIrepos = _feedbacksrepos;
        }
        // GET: api/<FeedbacksController>
        [HttpGet("get-all-feedbacks")]
        public IEnumerable<Feedbacks> GetAllFeedBacks()
        {
            return _feedbacksIrepos.GetAll();
        }
        // GET api/<FeedbacksController>/5
        [HttpGet("{id}")]
        public Feedbacks GetItem(Guid id)
        {
            return _feedbacksIrepos.GetItem(id);
        }
        [HttpGet("get-feedbacks-RatingStar/{RatingStar}")]
        public List<Feedbacks> GetFeedBacksbyRatingStar(int ratingStar)
        {
            return _feedbacksIrepos.GetAll().Where(p => p.RatingStar == ratingStar).ToList();
        }
        [HttpPost("create-feedbacks")]
        public bool CreateFeedBacks(Guid IdUser, Guid IdShoeDetail, string Note, int RatingStar)
        {
            Feedbacks feedbacks = new Feedbacks();
            feedbacks.Id = Guid.NewGuid();
            feedbacks.IdUser = IdUser;
            feedbacks.IdShoeDetail = IdShoeDetail;
            feedbacks.Note = Note;
            feedbacks.RatingStar = RatingStar;
            return _feedbacksIrepos.Create(feedbacks);
        }


        // PUT api/<FeedbacksController>/5
        [HttpPut("{edit-feedbacks-by-id}")]
        public bool UpdateFeedBacks(Guid id, Guid IdUser, Guid IdShoeDetail, string Note, int RatingStar)
        {
            var update = _feedbacksIrepos.GetAll().FirstOrDefault(x => x.Id == id);
            update.IdUser = IdUser;
            update.IdShoeDetail = IdShoeDetail;
            update.Note = Note;
            update.RatingStar = RatingStar;
            return _feedbacksIrepos.Update(update);
        }

        // DELETE api/<FeedbacksController>/5
        [HttpDelete("{delete-feedbacks-by-id}")]
        public bool Delete(Guid id)
        {
            var delete = _feedbacksIrepos.GetAll().FirstOrDefault(x => x.Id == id);
            return _feedbacksIrepos.Delete(delete);
        }
    }
}
