using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class ScopeConfiguration : BaseEntityConfiguration<Scope, Guid> {
        public override void Configure(EntityTypeBuilder<Scope> builder) {
            base.Configure(builder);

            builder
                .ToTable("Scopes");

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}