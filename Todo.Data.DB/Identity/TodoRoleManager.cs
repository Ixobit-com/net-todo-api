using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Identity {
    public class TodoRoleManager : RoleManager<Role> {
        public TodoRoleManager(
            TodoRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<TodoRoleManager> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger) { }
    }
}