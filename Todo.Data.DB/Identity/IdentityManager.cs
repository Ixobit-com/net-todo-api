using Microsoft.AspNetCore.Identity;
using Todo.Common.Constants;
using Todo.Common.Exceptions;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Identity {
    public class IdentityManager : IIdentityManager {
        private readonly TodoUserManager _userManager;
        private readonly TodoRoleManager _roleManager;
        private readonly TodoUserStore _userStore;

        public IdentityManager(
            TodoUserManager userManager,
            TodoRoleManager roleManager,
            TodoUserStore userStore) {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password) {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task CreateUserAsync(User user, string password, IEnumerable<Guid> roleIds) {
            using (var transaction = _userStore.BeginTransaction()) {
                try {
                    var result = await _userManager.CreateAsync(user, password);

                    EnsureIdentyResultSuccessed(result, ResultCodes.Errors.CREATE_USER_ERROR);

                    if (roleIds != null) {
                        await UpdateUserRolesAsync(user, roleIds, ResultCodes.Errors.CREATE_USER_ERROR);
                    }

                    transaction.Commit();
                }
                catch {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdateUserAsync(User user, IEnumerable<Guid> roleIds) {
            using (var transaction = _userStore.BeginTransaction()) {
                try {
                    var result = await _userManager.UpdateAsync(user);

                    EnsureIdentyResultSuccessed(result, ResultCodes.Errors.UPDATE_USER_ERROR);

                    if (roleIds != null) {
                        await UpdateUserRolesAsync(user, roleIds, ResultCodes.Errors.UPDATE_USER_ERROR);
                    }

                    transaction.Commit();
                }
                catch {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user) {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPasswordAsync(string email, string token, string newPassword) {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) {
                return;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            EnsureIdentyResultSuccessed(result, ResultCodes.Errors.RESET_PASSWORD_ERROR);
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword) {
            var user = await _userManager.FindByIdAsync($"{userId}");
            if (user == null) {
                return;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            EnsureIdentyResultSuccessed(result, ResultCodes.Errors.CHANGE_PASSWORD_ERROR);
        }

        private async Task UpdateUserRolesAsync(User user, IEnumerable<Guid> roleIds, string errorCode) {
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles?.Any() == true) {
                var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                EnsureIdentyResultSuccessed(result, errorCode);
            }

            if (roleIds?.Any() == true) {
                var roles = _roleManager.Roles
                    .Where(x => roleIds.Contains(x.Id))
                    .Select(x => x.Name)
                    .ToList();

                var result = await _userManager.AddToRolesAsync(user, roles);

                EnsureIdentyResultSuccessed(result, errorCode);
            }
        }

        private void EnsureIdentyResultSuccessed(IdentityResult result, string errorCode) {
            if (result.Succeeded) {
                return;
            }

            if (!result.Errors?.Any() ?? false) {
                throw new CodableException(errorCode, "Unknown error");
            }

            throw new CodableException(errorCode, String.Join(". ", result.Errors.Select(x => x.Description)));
        }
    }
}