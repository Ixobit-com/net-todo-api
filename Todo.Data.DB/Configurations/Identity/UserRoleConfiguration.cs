using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole> {
        public void Configure(EntityTypeBuilder<UserRole> builder) {
            builder
                .ToTable("UserRoles");
        }
    }
}