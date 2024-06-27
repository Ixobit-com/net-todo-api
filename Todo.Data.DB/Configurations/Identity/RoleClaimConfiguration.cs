using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim> {
        public void Configure(EntityTypeBuilder<RoleClaim> builder) {
            builder
                .ToTable("RoleClaims");
        }
    }
}