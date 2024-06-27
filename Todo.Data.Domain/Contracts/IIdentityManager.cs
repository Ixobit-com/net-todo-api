using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.Domain.Contracts {
    public interface IIdentityManager {
        Task<bool> CheckPasswordAsync(User user, string password);
        Task CreateUserAsync(User user, string password, IEnumerable<Guid> roleIds);
        Task UpdateUserAsync(User user, IEnumerable<Guid> roleIds);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task ResetPasswordAsync(string email, string token, string newPassword);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}