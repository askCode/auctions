using System;
namespace Bids.WebUI.DAL
{
    public interface IGenericRepository<TEntity>
     where TEntity : class
    {
        void Delete(int id);
        System.Collections.Generic.IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }
}
