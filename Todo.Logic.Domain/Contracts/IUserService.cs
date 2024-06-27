using Todo.Common;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.Logic.Domain.Contracts {
    public interface IUserService : IBaseCrudService<User, Guid, UserFilters> {
        Task<ServiceResult> ForgotPasswordAsync(string email);
        Task<ServiceResult> ResetPasswordAsync(string email, string token, string newPassword);
        Task<ServiceResult> ChangePasswordAsync(Guid id, string currentPassword, string newPassword);
    }
}