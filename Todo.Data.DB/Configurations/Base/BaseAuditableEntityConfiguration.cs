using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.DB.Configurations.Base {
    public class BaseAuditableEntityConfiguration<TEntity, TKey> : BaseEntityConfiguration<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, IAuditableEntity {
        public override void Configure(EntityTypeBuilder<TEntity> builder) {
            base.Configure(builder);

            builder
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
}