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
    public class CategoriesController : ControllerBase
    {
        // GET: api/<CategoriesController>
        private readonly IAllRepositories<Categories> _categoriesIRepos;

        private readonly AppDbContext _context = new AppDbContext();

        public CategoriesController()
        {
            var _categoriesRepos = new AllRepositories1<Categories>(this._context, this._context.Categories);
            this._categoriesIRepos = _categoriesRepos;
        }

        // create
        [HttpPost("create-category")]
        public bool CreateCategory(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) return false;

            // Check if brandName already exists
            if (this._categoriesIRepos.GetAll().Any(p => p.CategoryName.ToUpper().Trim() == categoryName.ToUpper().Trim()))
            {
                return false;
            }

            var category = new Categories();
            category.Id = Guid.NewGuid();
            category.CategoryName = categoryName;
            return this._categoriesIRepos.Create(category); // Create a new brand
        }

        // delete
        [HttpDelete("delete-category")]
        public bool DeleteCategory(Guid id)
        {
            var category = this._categoriesIRepos.GetAll().FirstOrDefault(p => p.Id == id);
            if (category == null)
            {
                Console.WriteLine("Category is not existed");
            }
            else
            {
                Console.WriteLine("Delete Done!");
                return this._categoriesIRepos.Delete(category);
            }

            return false;
        }

        [HttpDelete("delete-many-category")]
        public bool DeleteManyCategory(List<Guid> ids)
        {
            // cach 2 de xoa nhieu
            try
            {
                var categories = new List<Categories>();
                foreach (var item in ids)
                {
                    var category = this._categoriesIRepos.GetAll().FirstOrDefault(p => p.Id == item);
                    categories.Add(category);
                }

                return this._categoriesIRepos.DeleteMany(categories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // get
        [HttpGet("get-all-categories")]
        public IEnumerable<Categories> GetAllCategories()
        {
            return this._categoriesIRepos.GetAll();
        }

        [HttpGet("get-categories-by-id")]
        public Categories GetCategoriesById(Guid id)
        {
            return this._categoriesIRepos.GetAll().FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("get-categories-by-name")]
        public List<Categories> GetCategoriesByName(string categoryName)
        {
            return this._categoriesIRepos.GetAll().Where(p => p.CategoryName.Contains(categoryName)).ToList();
        }

        // update
        [HttpPut("update-category")]
        public bool UpdateCategory(Guid id, string categoryName)
        {
            if (this._categoriesIRepos.GetAll().Any(p => p.CategoryName.ToUpper().Trim() == categoryName.ToUpper().Trim()))
            {
                return false;
            }
            var category = this._categoriesIRepos.GetAll().FirstOrDefault(p => p.Id == id);
            category.CategoryName = categoryName;
            return this._categoriesIRepos.Update(category);
        }
    }
}