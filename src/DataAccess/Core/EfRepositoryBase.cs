using Data.Contexts.Sqlite;
using Entites.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Core
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (KartvizitProContextSqlite context=new KartvizitProContextSqlite())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public void Update(TEntity entity)
        {
            using (KartvizitProContextSqlite context = new KartvizitProContextSqlite())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (KartvizitProContextSqlite context = new KartvizitProContextSqlite())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Add(TEntity entity)
        {
            using (KartvizitProContextSqlite context = new KartvizitProContextSqlite())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (KartvizitProContextSqlite context = new KartvizitProContextSqlite())
            {
                if (filter != null)
                    return context.Set<TEntity>().Where(filter).ToList();
                else
                    return context.Set<TEntity>().ToList();
            }
        }
    }
}
