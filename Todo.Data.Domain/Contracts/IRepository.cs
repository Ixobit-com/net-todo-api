using System.Linq.Expressions;

namespace Todo.Data.Domain.Contracts {
    public interface IRepository { }

    public interface IRepository<TEntity> : IRepository
        where TEntity : class {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? expression = null, bool tracking = false);
        TEntity Add(TEntity item);
        IEnumerable<TEntity> Add(IEnumerable<TEntity> items);
        TEntity Update(TEntity item);
        IEnumerable<TEntity> Update(IEnumerable<TEntity> items);
        TEntity Delete(TEntity item);
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> items);
        bool Exist(Expression<Func<TEntity, bool>>? expression);
    }
}