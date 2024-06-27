using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.Logic.Domain.Contracts {
    public interface IScopeService : IBaseReadService<Scope, Guid, ScopeFilters> { }
}