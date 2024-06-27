using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken, Guid> {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder) {
            base.Configure(builder);

            builder
                .ToTable("RefreshTokens");
        }
    }
}