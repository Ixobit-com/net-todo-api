using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Common.Constants;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models;

namespace Todo.Data.DB.Configurations {
    public class LabelConfiguration : BaseAuditableEntityConfiguration<Label, Guid> {
        public override void Configure(EntityTypeBuilder<Label> builder) {
            base.Configure(builder);

            builder
                .ToTable("Labels");

            builder
                .Property(x => x.Name)
                .HasMaxLength(LabelConstants.NameMaxLength)
                .IsRequired();

            builder
                .HasIndex(x => x.Name)
                .HasFilter("IsDeleted = 0")
                .IsUnique();
        }
    }
}