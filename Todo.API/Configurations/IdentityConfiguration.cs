using Microsoft.AspNetCore.Identity;
using Todo.Data.DB.Context;
using Todo.Data.DB.Identity;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;

namespace Todo.API.Configurations {
    public static class IdentityConfiguration {
        public static void ConfigureIdentity(this IServiceCollection services) {
            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<TodoDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<TodoRoleStore>();
            services.AddTransient<TodoUserStore>();
            services.AddTransient<TodoRoleManager>();
            services.AddTransient<TodoUserManager>();
            services.AddTransient<TodoSignInManager>();

            services.AddTransient<IIdentityManager, IdentityManager>();
        }
    }
}