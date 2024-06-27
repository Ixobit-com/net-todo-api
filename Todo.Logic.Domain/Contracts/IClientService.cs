using Todo.Common;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.Logic.Domain.Contracts {
    public interface IClientService : IBaseCrudService<Client, Guid, ClientFilters> {
        Task<ServiceResult> ResetAsync(Guid id);
    }
}