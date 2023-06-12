using Data.IRepositories;
using Data.ShopContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AllRepositories1<T> : IAllRepositories<T> where T : class
    {
        private readonly AppDbContext _context;

        private readonly DbSet<T> _dbSet;

        public AllRepositories1()
        {
        }

        public AllRepositories1(AppDbContext context, DbSet<T> dbSet)
        {
            this._context = context;
            this._dbSet = dbSet;
        }

        public bool Create(T item)
        {
            try
            {
                this._dbSet.Add(item);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool CreateMany(List<T> items)
        {
            try
            {
                this._dbSet.AddRange(items);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Delete(T item)
        {
            try
            {
                this._dbSet.Remove(item);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeleteMany(List<T> items)
        {
            try
            {
                this._dbSet.RemoveRange(items);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetItem(Guid id)
        {
            return _dbSet.Find(id);
        }

        public bool Update(T item)
        {
            try
            {
                _dbSet.Update(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateMany(List<T> items)
        {
            try
            {
                this._dbSet.UpdateRange(items);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        }
}
