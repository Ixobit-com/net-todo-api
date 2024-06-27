using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Todo.Common;
using Todo.Common.Constants;
using Todo.Common.Email;
using Todo.Common.Email.Data;
using Todo.Common.Exceptions;
using Todo.Common.Helpers;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class UserService : BaseCrudService<User, Guid, UserFilters, UserOrderByField>, IUserService {
        private readonly IIdentityManager _identityManager;
        private readonly IEmailService _emailService;

        public UserService(
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            Logger logger,
            IMapper mapper,
            IIdentityManager identityManager,
            IEmailService emailService)
            : base(unitOfWork, logger, mapper) {
            _identityManager = identityManager;
            _emailService = emailService;
        }

        public async Task<ServiceResult> ForgotPasswordAsync(string email) {
            try {
                var user = _unitOfWork.GetRepository<User>()
                    .Get(x => x.Email == email)
                    .FirstOrDefault();

                await GenerateAndSendResetPasswordTokenAsync(user);

                return ServiceResult.Successed();
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Forgot password exception occured. Details: [{email}]");
                return ServiceResult.InternalServerError(ex.Message);
            }
        }

        public async Task<ServiceResult> ResetPasswordAsync(string email, string token, string newPassword) {
            try {
                await _identityManager.ResetPasswordAsync(email, token, newPassword);

                return ServiceResult.Successed();
            }
            catch (CodableException ex) {
                _logger.Error(ex, $"Reset password exception occured. Details: [{email}, {token}, {newPassword}]");
                return ServiceResult.Failed(ex.Code, ex.Message);
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Reset password exception occured. Details: [{email}, {token}, {newPassword}]");
                return ServiceResult.InternalServerError(ex.Message);
            }
        }

        public async Task<ServiceResult> ChangePasswordAsync(Guid id, string currentPassword, string newPassword) {
            try {
                await _identityManager.ChangePasswordAsync(id, currentPassword, newPassword);

                return ServiceResult.Successed();
            }
            catch (CodableException ex) {
                _logger.Error(ex, $"Change password exception occured. Details: [{id}, {currentPassword}, {newPassword}]");
                return ServiceResult.Failed(ex.Code, ex.Message);
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Change password exception occured. Details: [{id}, {currentPassword}, {newPassword}]");
                return ServiceResult.InternalServerError(ex.Message);
            }
        }

        protected override Guid GetNewKey() {
            return Guid.NewGuid();
        }

        protected override async Task OnCreatingAsync<TCreateDto>(User entity, TCreateDto dto) {
            string password = PasswordHelper.GeneratePassword(24, 12);

            // Store it in DTO to set it later via user manager
            dto.SetMetadata(MetadataNames.User.Password, password);
        }

        protected override async Task OnCreatedAsync<TCreateDto>(User entity, TCreateDto dto) {
            await GenerateAndSendResetPasswordTokenAsync(entity);
        }

        protected override async Task SaveChangesOnCreateAsync<TCreateDto>(User entity, TCreateDto dto) {
            string password = dto.GetMetadata<string>(MetadataNames.User.Password);
            IEnumerable<Guid> roleIds = null;

            if (dto is IHasRoles) {
                roleIds = (dto as IHasRoles)?.Roles?.Select(x => x.Id) ?? new List<Guid>();
            }

            await _identityManager.CreateUserAsync(entity, password, roleIds);
        }

        protected override async Task SaveChangesOnUpdateAsync<TUpdateDto>(User entity, TUpdateDto dto) {
            IEnumerable<Guid> roleIds = null;

            if (dto is IHasRoles) {
                roleIds = (dto as IHasRoles)?.Roles?.Select(x => x.Id) ?? new List<Guid>();
            }

            await _identityManager.UpdateUserAsync(entity, roleIds);
        }

        protected override List<Expression<Func<User, bool>>>? GetAdvancedConditions(UserFilters filters) {
            var conditions = new List<Expression<Func<User, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x =>
                    x.Email.ToLower().Contains(filters.SearchText.ToLower()) ||
                    x.FirstName.ToLower().Contains(filters.SearchText.ToLower()) ||
                    x.LastName.ToLower().Contains(filters.SearchText.ToLower()));
            }

            if (filters.RoleIds?.Any() == true) {
                conditions.Add(x => x.UserRoles.Any(ur => filters.RoleIds.Contains(ur.RoleId)));
            }

            return conditions;
        }

        protected override IQueryable<User> OrderBy(IQueryable<User> query, UserOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case UserOrderByField.Name:
                    return (desc) ? query.OrderByDescending(x => x.LastName).ThenByDescending(x => x.FirstName) : query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }

        private async Task GenerateAndSendResetPasswordTokenAsync(User user) {
            if (user == null) {
                return;
            }

            var token = await _identityManager.GeneratePasswordResetTokenAsync(user);

            await _emailService.SendResetUserPasswordEmailAsync(
                new ResetUserPasswordEmailData {
                    Token = token
                },
                new Recipient($"{user.FirstName} {user.LastName}", user.Email));
        }
    }
}