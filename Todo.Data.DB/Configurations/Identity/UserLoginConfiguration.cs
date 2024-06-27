using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin> {
        public void Configure(EntityTypeBuilder<UserLogin> builder) {
            builder
                .ToTable("UserLogins");
        }
    }
}