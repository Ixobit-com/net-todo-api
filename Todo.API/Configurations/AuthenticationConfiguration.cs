using Microsoft.IdentityModel.Tokens;
using System.Text;
using Todo.Common.Settings;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Services;

namespace Todo.API.Configurations {
    public static class AuthenticationConfiguration {
        public static void ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configuration) {
            var section = configuration.GetSection("Jwt");

            services
                .AddAuthentication("Bearer")
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidIssuer = section["Issuer"],
                        ValidAudience = section["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section["Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            services.AddScoped<IJwtService, JwtService>();

            services.Configure<JwtSettings>(section);
        }
    }
}