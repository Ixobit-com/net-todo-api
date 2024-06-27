using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class RoleConfiguration : BaseEntityConfiguration<Role, Guid> {
        public override void Configure(EntityTypeBuilder<Role> builder) {
            base.Configure(builder);

            builder
                .ToTable("Roles");

            builder
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}