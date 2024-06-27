using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Common.Constants;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models;

namespace Todo.Data.DB.Configurations {
    public class TicketConfiguration : BaseAuditableEntityConfiguration<Ticket, Guid> {
        public override void Configure(EntityTypeBuilder<Ticket> builder) {
            base.Configure(builder);

            builder
                .ToTable("Tickets");

            builder
                .Property(x => x.Subject)
                .HasMaxLength(TicketConstants.SubjectMaxLength)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(TicketConstants.DescriptionMaxLength);
        }
    }
}