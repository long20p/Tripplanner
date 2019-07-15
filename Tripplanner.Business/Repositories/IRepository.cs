using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Tripplanner.Business.Repositories
{
    public interface IRepository<T> where T : new()
    {
        bool Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> filter);
        bool Delete(T entity);
    }
}
