using Microsoft.OpenApi.Models;
using Todo.API.Filters;

namespace Todo.API.Configurations {
    public static class SwaggerConfiguration {
        public static void ConfigureSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(options => {
                options.CustomSchemaIds(type => type.ToString());

                options.DocumentFilter<JsonPatchDocumentFilter>();

                options.DescribeAllParametersInCamelCase();

                options.SwaggerDoc("v1",
                    new OpenApiInfo {
                        Title = "To do API",
                        Version = "v1"
                    });

                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, "Todo.API.xml");
                options.IncludeXmlComments(xmlFilePath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}