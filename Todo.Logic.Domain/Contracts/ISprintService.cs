using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.Logic.Domain.Contracts {
    public interface ISprintService : IBaseCrudService<Sprint, Guid, SprintFilters> { }
}