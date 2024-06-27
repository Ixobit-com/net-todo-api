using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Todo.Data.DB.Context;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Identity {
    public class TodoRoleStore : RoleStore<Role, TodoDbContext, Guid, UserRole, RoleClaim> {
        public TodoRoleStore(
            TodoDbContext context)
            : base(context) {
            AutoSaveChanges = false;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}