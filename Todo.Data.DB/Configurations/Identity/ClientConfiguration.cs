using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Common.Constants;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class ClientConfiguration : BaseAuditableEntityConfiguration<Client, Guid> {
        public override void Configure(EntityTypeBuilder<Client> builder) {
            base.Configure(builder);

            builder
                .ToTable("Clients");

            builder
                .Property(x => x.Name)
                .HasMaxLength(ClientConstants.NameMaxLength)
                .IsRequired();

            builder
                .Property(x => x.Key)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(x => x.Key)
                .HasFilter("IsDeleted = 0")
                .IsUnique();

            builder
                .Property(x => x.SecretSalt)
                .IsRequired();
        }
    }
}