using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface IRepository<T> where T: BaseEntityModel
    {
        T GetById(long id);
        T Add(T entity);
        Task<bool> DeleteByIdAsync(long id);

        Task<T> Update(T entity);

        IEnumerable<T> Find(Func<T, bool> searchCriteria); 

    }
}
