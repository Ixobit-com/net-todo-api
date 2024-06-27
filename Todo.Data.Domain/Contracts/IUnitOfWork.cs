namespace Todo.Data.Domain.Contracts {
    public interface IUnitOfWork {
        IRepository<T> GetRepository<T>() where T : class;
        void Commit();
        Task CommitAsync();
    }
}