using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken> {
        public void Configure(EntityTypeBuilder<UserToken> builder) {
            builder
                .ToTable("UserTokens");
        }
    }
}