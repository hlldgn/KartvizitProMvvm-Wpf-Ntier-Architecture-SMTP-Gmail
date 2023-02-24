using Core.Results;
using Entites.Abstract;

namespace Business.Abstract
{
    public interface IBaseService<TEntity> where TEntity : class, IEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Search(string search);
        IResult Add(TEntity entity);
        IResult Delete(TEntity entity);
        IResult Update(TEntity entity);
    }
}

