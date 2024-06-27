using Todo.Common;
using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Contracts {
    public interface IBaseCrudService<TEntity, TKey, TFilters> : IBaseUpdateService<TEntity, TKey, TFilters>
        where TKey : IEquatable<TKey>, new() {
        Task<ServiceResult<T>> CreateAsync<T, TCreateDto>(TCreateDto dto) where TCreateDto : BaseEntityDto<TKey>;
        Task<ServiceResult> DeleteAsync(TKey id);
    }
}