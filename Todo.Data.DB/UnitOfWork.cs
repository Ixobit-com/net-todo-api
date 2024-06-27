using Todo.Data.DB.Context;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.DB {
    public class UnitOfWork : IUnitOfWork {
        protected readonly TodoDbContext _context;
        private readonly IDictionary<string, IRepository> _repositories;

        public UnitOfWork(
            TodoDbContext context) {
            _context = context;
            _repositories = new SortedDictionary<string, IRepository>();
        }

        public IRepository<T> GetRepository<T>() where T : class {
            var typeName = typeof(T).FullName;

            if (!_repositories.ContainsKey(typeName)) {
                _repositories.Add(typeName, new Repository<T>(_context));
            }

            return (IRepository<T>)_repositories[typeName];
        }

        public void Commit() {
            _context.SaveChanges();
        }

        public async Task CommitAsync() {
            await _context.SaveChangesAsync();
        }
    }
}