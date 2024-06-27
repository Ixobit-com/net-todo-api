using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.Logic.Domain.Contracts {
    public interface ILabelService : IBaseCrudService<Label, Guid, LabelFilters> { }
}