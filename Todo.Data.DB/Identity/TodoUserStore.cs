using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Todo.Data.DB.Context;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Identity {
    public class TodoUserStore : UserStore<User, Role, TodoDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim> {
        public TodoUserStore(
            TodoDbContext context)
            : base(context) {
            //AutoSaveChanges = false;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) {
            //await Context.SaveChangesAsync(cancellationToken);
        }

        public IDbContextTransaction BeginTransaction() {
            return Context.Database.BeginTransaction();
        }
    }
}