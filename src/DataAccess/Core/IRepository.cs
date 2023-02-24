using Entites.Abstract;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Core
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetList(Expression<Func<T, bool>>? filter=null);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
    }
}
