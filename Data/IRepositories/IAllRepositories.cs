using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepositories
{
    public interface IAllRepositories<T> 
    {
        public bool Create(T item);

        public bool CreateMany(List<T> items);

        public bool Delete(T item);

        public bool DeleteMany(List<T> items);

        public IEnumerable<T> GetAll();

        public T GetItem(Guid id);

        public bool Update(T item);

        public bool UpdateMany(List<T> items);
        

  
    }
}
