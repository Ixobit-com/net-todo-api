using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Configurations.Identity {
    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope> {
        public void Configure(EntityTypeBuilder<ClientScope> builder) {
            builder
                .ToTable("ClientScopes");

            builder
                .HasKey(x => new { x.ClientId, x.ScopeId });
        }
    }
}