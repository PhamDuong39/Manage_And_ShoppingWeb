using Data.IRepositories;
using Data.Models;
using Data.Repositories;
using Data.ShopContext;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        public IAllRepositories<Sales> _irepos;
        AppDbContext DbContext;

        public SalesController()
        {
            DbContext = new AppDbContext();
            AllRepositories1<Sales> repos = new AllRepositories1<Sales>(DbContext, DbContext.Sales);
            _irepos = repos;

        }
        [HttpGet]
        
        // GET: api/<BillController>
        [HttpGet("Show-Sales")]
        public IEnumerable<Sales> GetAllSales()
        {
            return _irepos.GetAll();
        }


        [HttpGet("{id}")]
        public Sales Get(Guid id)
        {
            return _irepos.GetAll().First(p => p.Id == id);
        }
        [HttpPost("Create-Sales")]
        public bool CreateSales(int DiscountValue, string SaleName)
        {

            Sales sale = new Sales();
            sale.DiscountValue = DiscountValue;
            sale.SaleName = SaleName;
            sale.StartDate = DateTime.Now;
            sale.EndDate = DateTime.Now.AddDays(7);

            string formattedStartDate = sale.StartDate.ToString("yyyy-MM-ddTHH:mm:ss");
            string formattedEndDate = sale.EndDate.ToString("yyyy-MM-ddTHH:mm:ss");

            return _irepos.Create(sale);
        }

        [HttpPut("EditSales")]
        public bool UpdateSales(Guid id, int DiscountValue, string SaleName, DateTime EndDate)
        {
            var sale = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
            sale.DiscountValue = DiscountValue;
            sale.SaleName = SaleName;
            //sale.EndDate = DateTime.ParseExact(EndDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            sale.EndDate = EndDate;
            return _irepos.Update(sale);
        }

        [HttpDelete("Delete-Sales-/{id}")]  
        public bool Delete(Guid id)
        {
            Sales sale = _irepos.GetAll().FirstOrDefault(p => p.Id == id);
            return _irepos.Delete(sale);
        }
    }
}
