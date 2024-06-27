using Todo.Common;

namespace Todo.Logic.Domain.Contracts {
    public interface IBaseReadService<TEntity, TKey, TFilters> {
        Task<ServiceResult<PagingResult<T>>> GetAsync<T>(TFilters filters);
        Task<ServiceResult<T>> FindAsync<T>(TKey id);
    }
}