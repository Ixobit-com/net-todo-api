using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Data.DB.Configurations;
using Todo.Data.DB.Configurations.Identity;
using Todo.Data.Domain.Models;
using Todo.Data.Domain.Models.Identity;

namespace Todo.Data.DB.Context {
    public class TodoDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken> {
        public TodoDbContext(
            DbContextOptions<TodoDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketLabel> TicketLabels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            // Identity
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new ClientScopeConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new ScopeConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserLoginConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserTokenConfiguration());

            // Entities
            builder.ApplyConfiguration(new EmailTemplateConfiguration());
            builder.ApplyConfiguration(new LabelConfiguration());
            builder.ApplyConfiguration(new SprintConfiguration());
            builder.ApplyConfiguration(new TicketConfiguration());
            builder.ApplyConfiguration(new TicketLabelConfiguration());
        }
    }
}