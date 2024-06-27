using Todo.Common;
using Todo.Logic.Domain.Models.Base;

namespace Todo.Logic.Domain.Contracts {
    public interface IBaseUpdateService<TEntity, TKey, TFilters> : IBaseReadService<TEntity, TKey, TFilters>
        where TKey : IEquatable<TKey>, new() {
        Task<ServiceResult<T>> UpdateAsync<T, TUpdateDto>(TUpdateDto dto) where TUpdateDto : BaseEntityDto<TKey>;
    }
}