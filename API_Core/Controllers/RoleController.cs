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
    public class RoleController : ControllerBase
    {
        private IAllRepositories<Roles> _roleIrepos;
        private IAllRepositories<Users> _userIRepos;
        private readonly AppDbContext _context = new();
        public RoleController()
        {
            var _rolerepos = new AllRepositories1<Roles>(_context, _context.Roles);
            _roleIrepos = _rolerepos;

            var _userrepos = new AllRepositories1<Users>(_context, _context.users);
            _userIRepos = _userrepos;
        }
        [HttpGet("get-all-role")]
        public IEnumerable<Roles> GetAllRoles()
        {
            return _roleIrepos.GetAll();
        }
        [HttpGet("{id}")]
        public Roles GetByIdRole(Guid id)
        {
            return _roleIrepos.GetItem(id);
        }
        [HttpPost("create-role")]
        public bool CreateRole(string RoleName, int Status)
        {
            Roles role = new Roles();
            role.Id = Guid.NewGuid();
            role.RoleName = RoleName;
            role.Status = Status;
            return _roleIrepos.Create(role);
        }
        [HttpDelete("delete-role-by-id")]
        public bool DeleteRole(Guid id)
        {
            var DeleteRole = _roleIrepos.GetAll().FirstOrDefault(x => x.Id == id);
            return _roleIrepos.Delete(DeleteRole);
        }
        [HttpDelete("delete-many-role")]
        public bool DeleteManyRole(List<Guid> id)
        {
            List<Roles> roles = _roleIrepos.GetAll().Where(x => id.Contains(x.Id)).ToList();
            return _roleIrepos.DeleteMany(roles);
        }
        [HttpPut("Edit-role-by-id")]
        public bool UpdateRole(Guid id, string RoleName, int Status)
        {
            var update = _roleIrepos.GetAll().FirstOrDefault(x => x.Id == id);
            update.RoleName = RoleName;
            update.Status = Status;
            return _roleIrepos.Update(update);
        }

        [HttpGet("IsAdmin")]
        public Roles CheckRole(string username)
        {
            var user = _userIRepos.GetAll().FirstOrDefault(p => p.Username == username);
            //if (user == null) return false;
            var role = _roleIrepos.GetAll().FirstOrDefault(p => p.Id == user.IdRole);
            return role;
        }
    }
}
