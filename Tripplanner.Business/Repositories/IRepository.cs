using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Tripplanner.Business.Repositories
{
    public interface IRepository<T> where T : new()
    {
        bool Add(T entity);
        T GetById(int id);
        T GetByPredicate(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> filter);
        bool Delete(T entity);
        bool Update(T entity);
        bool AddOrReplace(IEnumerable<T> entities);
        bool AddOrUpdateSingle(T entity);
    }
}
