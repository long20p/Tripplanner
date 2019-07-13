using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Repositories
{
    public interface IRepository<T> where T : new()
    {
        bool Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        bool Delete(T entity);
    }
}
