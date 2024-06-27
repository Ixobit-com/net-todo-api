using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Common.Constants;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class UserConfiguration : BaseEntityConfiguration<User, Guid> {
        public override void Configure(EntityTypeBuilder<User> builder) {
            base.Configure(builder);

            builder
                .ToTable("Users");

            builder
                .Property(x => x.Email)
                .HasMaxLength(UserConstants.EmailMaxLength)
                .IsRequired();

            builder
                .Property(x => x.FirstName)
                .HasMaxLength(UserConstants.FirstNameMaxLength)
                .IsRequired();

            builder
                .Property(x => x.LastName)
                .HasMaxLength(UserConstants.LastNameMaxLength)
                .IsRequired();

            builder
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}