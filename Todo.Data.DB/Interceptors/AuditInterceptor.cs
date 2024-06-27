using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Todo.Data.DB.Context;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.DB.Interceptors {
    public class AuditInterceptor : SaveChangesInterceptor {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) {
            AuditProcess(eventData.Context as TodoDbContext);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default) {
            AuditProcess(eventData.Context as TodoDbContext);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AuditProcess(TodoDbContext dbContext) {
            if (dbContext == null) {
                return;
            }

            var entries = dbContext.ChangeTracker
                .Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
                .ToList();

            if (entries == null || !entries.Any()) {
                return;
            }

            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries) {
                IAuditableEntity auditableEntity = (IAuditableEntity)entry.Entity;

                switch (entry.State) {
                    case EntityState.Added: {
                            auditableEntity.CreatedAt = utcNow;
                            break;
                        }
                    case EntityState.Modified: {
                            dbContext.Entry(auditableEntity).Property(p => p.CreatedAt).IsModified = false;
                            auditableEntity.UpdatedAt = utcNow;
                            break;
                        }
                    case EntityState.Deleted: {
                            dbContext.Entry(auditableEntity).Property(p => p.CreatedAt).IsModified = false;

                            entry.State = EntityState.Modified;

                            auditableEntity.IsDeleted = true;
                            auditableEntity.DeletedAt = utcNow;
                            break;
                        }
                }
            }
        }
    }
}