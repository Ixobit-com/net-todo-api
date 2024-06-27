using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Data.DB.Configurations.Base;
using Todo.Data.Domain.Models;

namespace Todo.Data.DB.Configurations {
    public class EmailTemplateConfiguration : BaseAuditableEntityConfiguration<EmailTemplate, int> {
        public override void Configure(EntityTypeBuilder<EmailTemplate> builder) {
            base.Configure(builder);

            builder
                .ToTable("EmailTemplates");
        }
    }
}