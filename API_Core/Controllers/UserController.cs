using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        // CC
        private IAllRepositories<Users> _userIrepos;
        private IAllRepositories<Roles> _roleIrepos;
        private readonly AppDbContext _context = new();

        public UserController()
        {
            var _userrepos = new AllRepositories1<Users>(_context, _context.users);
            _userIrepos = _userrepos;
            var _rolerepos = new AllRepositories1<Roles>(_context, _context.Roles);
            _roleIrepos = _rolerepos;
        }

        [HttpGet("get-all-user")]
        public IEnumerable<Users> GetAllUser()
        {
            return _userIrepos.GetAll();
        }

        [HttpGet("{id}")]
        public Users GetItem(Guid id)
        {
            return _userIrepos.GetItem(id);
        }

        [HttpGet("get-user-by-name/{name}")]
        public List<Users> GetColorByName(string name)
        {
            return _userIrepos.GetAll().Where(p => p.Username == name).ToList();
        }

        [HttpPost("create-user")]
        public bool CreateUser(
            Guid idrole,
            string Username,
            string Password,
            string Address,
            string Phonenumber,
            string Email,
            int Status,
            string Fullname
        )
        {
            Users user = new Users();
            user.Id = Guid.NewGuid();
            user.Username = Username;
            user.Password = Password;
            user.Address = Address;
            user.Phonenumber = Phonenumber;
            user.Email = Email;
            user.Status = Status;
            user.Fullname = Fullname;
            user.IdRole = idrole;
            return _userIrepos.Create(user);

        }

        [HttpDelete("delete-user-by-id")]
        public bool DeleteUser(Guid Id)
        {
            var Delete = _userIrepos.GetAll().FirstOrDefault(x => x.Id == Id);
            return _userIrepos.Delete(Delete);
        }

        [HttpDelete("Delete-many-user")]
        public bool DeleteManyUser(List<Guid> Id)
        {
            List<Users> colors = _userIrepos.GetAll().Where(p => Id.Contains(p.Id)).ToList();
            return _userIrepos.DeleteMany(colors);
        }

        [HttpPut("update-user-by-id")]
        public bool UpdateUser(
            Guid id,
            string Username,
            string Password,
            string Address,
            string Phonenumber,
            string Email,
            int Status,
            string Fullname,
            Guid IdRole
        )
        {
            var update = _userIrepos.GetAll().FirstOrDefault(i => i.Id == id);
            update.Username = Username;
            update.Password = Password;
            update.Address = Address;
            update.Email = Email;
            update.Status = Status;
            update.Fullname = Fullname;
            update.IdRole = IdRole;
            return _userIrepos.Update(update);
        }
        
        [HttpGet("Login")]
        public bool IsConfirmUser(string username, string password)
        {
            var user = _userIrepos.GetAll().FirstOrDefault(p => p.Username == username && p.Password == password);
            if (user == null) return false;
            else return true;
        }
    }
}
