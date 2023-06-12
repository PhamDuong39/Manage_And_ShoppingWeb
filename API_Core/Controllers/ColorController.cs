

namespace API_Core.Controllers
{

    using Data.IRepositories;
    using Data.Models;
    using Data.Repositories;
    using Data.ShopContext;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : Controller
    {
        private readonly IAllRepositories<Colors> _colorIrepos;

        private readonly AppDbContext _context = new AppDbContext();

        public ColorController()
        {
            var _colorRepos = new AllRepositories1<Colors>(this._context, this._context.Colors);
            this._colorIrepos = _colorRepos;
        }

        [HttpPost("create-color")]
        public bool CreateColor(string colorName)
        {
            if (string.IsNullOrEmpty(colorName)) return false;

            // Check if brandName already exists
            if (this._colorIrepos.GetAll().Any(p => p.ColorName.ToUpper().Trim() == colorName.ToUpper().Trim()))
            {
                return false;
            }
            var color = new Colors();
            color.Id = Guid.NewGuid();
            color.ColorName = colorName;
            return this._colorIrepos.Create(color); // tạo màu mới
        }

        [HttpDelete("delete-color-by-id")]
        public bool DeleteColor(Guid Id)
        {
            var colorDelete = this._colorIrepos.GetAll().FirstOrDefault(i => i.Id == Id); // lấy màu có id tương ứng
            return this._colorIrepos.Delete(colorDelete); // xóa màu
        }

        [HttpDelete("delete-many-color")]
        public bool DeleteManyColor(List<Guid> Id)
        {
            var colors = new List<Colors>();
            foreach (var id in Id)
            {
                var color = this._colorIrepos.GetAll().FirstOrDefault(i => i.Id == id); // lấy màu có id tương ứng
                colors.Add(color);
            }
            return this._colorIrepos.DeleteMany(colors); // xóa nhiều màu
        }

        // GET : api/Colors
        [HttpGet("get-all-colors")]
        public IEnumerable<Colors> GetAllColors()
        {
            return this._colorIrepos.GetAll(); // trả về danh sách tất cả các màu
        }

        [HttpGet("get-color-by-id/{id}")]
        public Colors GetColorById(Guid id)
        {
            return this._colorIrepos.GetItem(id); // trả về màu có id tương ứng
        }

        [HttpGet("get-color-by-name/{name}")]
        public List<Colors> GetColorByName(string name)
        {
            return this._colorIrepos.GetAll().Where(p => p.ColorName.Contains(name)).ToList();
        }

        [HttpPut("update-color-by-id")]
        public bool UpdateColor(Guid Id, string colorName)
        {
            //Kieemr tra xem co ton tai hay khong
            if (string.IsNullOrEmpty(colorName)) return false;
            if (this._colorIrepos.GetAll().Any(p => p.ColorName.ToUpper().Trim() == colorName.ToUpper().Trim()))
            {
                return false;
            }
            var colorUpdate = this._colorIrepos.GetAll().FirstOrDefault(i => i.Id == Id); // lấy màu có id tương ứng
            colorUpdate.ColorName = colorName; // cập nhật tên màu
            return this._colorIrepos.Update(colorUpdate); // cập nhật màu
        }

        // Dung update all cung duoc nhung phai truyen du lieu day du len
        // Han Che dung update all vi phai truyen du lieu day du len
        // [HttpPut("update-many-colors")]
        // public bool UpdateManyColors(List<Guid> Id, List<string> colorName)
        // {
        // try
        // {
        // var colors = new List<Colors>();

        // for (var i = 0; i < Id.Count; i++)
        // {
        // var color = new Colors { Id = Id[i], ColorName = colorName[i] };

        // colors.Add(color);
        // }

        // return this._colorIrepos.UpdateMany(colors);
        // }
        // catch (Exception e)
        // {
        // Console.WriteLine(e.Message); // nen dung try catch de bao loi
        // return false;
        // }
        // }
    }
}