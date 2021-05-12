using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_mgmnt.Contracts
{
    public interface IRepositoryBase<T> where T:class
    {
        ICollection<T> fndAll();
        T FindById(int id);
        bool isExists(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
