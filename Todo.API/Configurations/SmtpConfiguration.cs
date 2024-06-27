using Todo.Common.Settings;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Services;

namespace Todo.API.Configurations {
    public static class SmtpConfiguration {
        public static void ConfigureSmtp(this IServiceCollection services, ConfigurationManager configuration) {
            var section = configuration.GetSection("Smtp");

            services.AddScoped<IEmailService, EmailService>();

            services.Configure<SmtpSettings>(section);
        }
    }
}