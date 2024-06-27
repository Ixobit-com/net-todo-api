using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models;

namespace Todo.Data.DB.Configurations {
    public class TicketLabelConfiguration : IEntityTypeConfiguration<TicketLabel> {
        public void Configure(EntityTypeBuilder<TicketLabel> builder) {
            builder
                .ToTable("TicketLabels");

            builder
                .HasKey(x => new { x.TicketId, x.LabelId });
        }
    }
}