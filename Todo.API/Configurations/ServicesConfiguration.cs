using Todo.Logic.Domain.Contracts;
using Todo.Logic.Services;

namespace Todo.API.Configurations {
    public static class ServicesConfiguration {
        public static void ConfigureServices(this IServiceCollection services) {
            services.AddTransient<IScopeService, ScopeService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<ILabelService, LabelService>();
            services.AddTransient<ISprintService, SprintService>();
            services.AddTransient<ITicketService, TicketService>();
        }
    }
}