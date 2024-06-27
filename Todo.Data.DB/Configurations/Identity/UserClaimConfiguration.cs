using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim> {
        public void Configure(EntityTypeBuilder<UserClaim> builder) {
            builder
                .ToTable("UserClaims");
        }
    }
}