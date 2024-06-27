using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Common.Constants;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models;

namespace Todo.Data.DB.Configurations {
    public class SprintConfiguration : BaseAuditableEntityConfiguration<Sprint, Guid> {
        public override void Configure(EntityTypeBuilder<Sprint> builder) {
            base.Configure(builder);

            builder
                .ToTable("Sprints");

            builder
                .Property(x => x.Name)
                .HasMaxLength(SprintConstants.NameMaxLength)
                .IsRequired();
        }
    }
}