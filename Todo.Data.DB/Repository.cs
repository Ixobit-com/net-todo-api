using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.DB {
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class {
        protected DbSet<TEntity> _data;
        protected DbContext _context;

        public Repository(
            DbContext context) {
            _context = context;
            _data = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? expression = null, bool tracking = false) {
            var data = tracking ? _data.AsTracking() : _data.AsNoTracking();

            if (expression != null) {
                data = data.Where(expression);
            }

            return data;
        }

        public TEntity Add(TEntity item) {
            _data.Add(item);

            return item;
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> items) {
            _data.AddRange(items);

            return items;
        }

        public TEntity Update(TEntity item) {
            _context.Entry(item).State = EntityState.Modified;

            return item;
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> items) {
            _data.UpdateRange(items);

            return items;
        }

        public TEntity Delete(TEntity item) {
            _data.Remove(item);

            return item;
        }

        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> items) {
            _data.RemoveRange(items);

            return items;
        }

        public bool Exist(Expression<Func<TEntity, bool>>? expression) {
            return _data
                .AsNoTracking()
                .Any(expression);
        }
    }
}