using Microsoft.EntityFrameworkCore;
using Todo.Common.Constants;
using Todo.Common.Helpers;
using Todo.Data.DB;
using Todo.Data.DB.Constants;
using Todo.Data.DB.Context;
using Todo.Data.DB.Identity;
using Todo.Data.DB.Interceptors;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;

namespace Todo.API.Configurations {
    public static class DatabaseConfiguration {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionString) {
            services.AddSingleton<AuditInterceptor>();

            services.AddDbContext<TodoDbContext>((serviceProvider, options) => {
                var auditInterceptor = serviceProvider.GetService<AuditInterceptor>();

                options
                    .UseSqlServer(connectionString)
                    .AddInterceptors(auditInterceptor);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void ApplyMigrations(this IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.CreateScope()) {
                serviceScope.ServiceProvider
                    .GetService<TodoDbContext>()
                    .Database.Migrate();
            }
        }

        public static async Task SeedAsync(this IApplicationBuilder app, ConfigurationManager configuration) {
            using (var serviceScope = app.ApplicationServices.CreateScope()) {
                var serviceProvider = serviceScope.ServiceProvider;

                await SeedRolesAsync(serviceProvider);
                await SeedUsersAsync(serviceProvider);
                await SeedScopesAsync(serviceProvider);
                await SeedClientsAsync(serviceProvider, configuration.GetSection("ClientHasher"));
            }
        }

        private static async Task SeedRolesAsync(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<TodoRoleManager>();
            var roleStore = serviceProvider.GetRequiredService<TodoRoleStore>();

            var roles = new List<Role> {
                new Role {
                    Id = DefaultIds.Role.AdministratorId,
                    Name = RoleNames.Administrator
                },
                new Role {
                    Id = DefaultIds.Role.UserId,
                    Name = RoleNames.User
                }
            };

            foreach (var role in roles) {
                if (await roleManager.RoleExistsAsync(role.Name)) {
                    continue;
                }

                await roleManager.CreateAsync(role);
            }

            await roleStore.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(IServiceProvider serviceProvider) {
            var userManager = serviceProvider.GetRequiredService<TodoUserManager>();
            var userStore = serviceProvider.GetRequiredService<TodoUserStore>();

            string adminEmail = "admin@todo.com";

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null) {
                user = new User {
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                await userManager.CreateAsync(user, "1qa@WS3ed");
            }

            var roles = new List<string> {
                RoleNames.Administrator
            };

            foreach (var role in roles) {
                if (await userManager.IsInRoleAsync(user, role)) {
                    continue;
                }

                await userManager.AddToRoleAsync(user, role);
            }

            await userStore.SaveChangesAsync();
        }

        private static async Task SeedScopesAsync(IServiceProvider serviceProvider) {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            var scopes = new List<Scope> {
                // Scope
                new Scope {
                    Id = DefaultIds.Scope.ScopeReadId,
                    Name = ScopeNames.ScopeRead,
                    Description = "This scope allows to get scopes"
                },
                // Role
                new Scope {
                    Id = DefaultIds.Scope.RoleReadId,
                    Name = ScopeNames.RoleRead,
                    Description = "This scope allows to get roles"
                },
                // Client
                new Scope {
                    Id = DefaultIds.Scope.ClientCreateId,
                    Name = ScopeNames.ClientCreate,
                    Description = "This scope allows to create a new client"
                },
                new Scope {
                    Id = DefaultIds.Scope.ClientReadId,
                    Name = ScopeNames.ClientRead,
                    Description = "This scope allows to get clients"
                },
                new Scope {
                    Id = DefaultIds.Scope.ClientUpdateId,
                    Name = ScopeNames.ClientUpdate,
                    Description = "This scope allows to update an existing client"
                },
                new Scope {
                    Id = DefaultIds.Scope.ClientDeleteId,
                    Name = ScopeNames.ClientDelete,
                    Description = "This scope allows to delete an existing client"
                },
                new Scope {
                    Id = DefaultIds.Scope.ClientResetId,
                    Name = ScopeNames.ClientReset,
                    Description = "This scope allows to reset client credentials"
                },
                // User
                new Scope {
                    Id = DefaultIds.Scope.UserCreateId,
                    Name = ScopeNames.UserCreate,
                    Description = "This scope allows to create a new user"
                },
                new Scope {
                    Id = DefaultIds.Scope.UserReadId,
                    Name = ScopeNames.UserRead,
                    Description = "This scope allows to get users"
                },
                new Scope {
                    Id = DefaultIds.Scope.UserUpdateId,
                    Name = ScopeNames.UserUpdate,
                    Description = "This scope allows to update an existing user"
                },
                new Scope {
                    Id = DefaultIds.Scope.UserDeleteId,
                    Name = ScopeNames.UserDelete,
                    Description = "This scope allows to delete an existing user"
                },
                // Sprint
                new Scope {
                    Id = DefaultIds.Scope.SprintCreateId,
                    Name = ScopeNames.SprintCreate,
                    Description = "This scope allows to create a new sprint"
                },
                new Scope {
                    Id = DefaultIds.Scope.SprintReadId,
                    Name = ScopeNames.SprintRead,
                    Description = "This scope allows to get sprints"
                },
                new Scope {
                    Id = DefaultIds.Scope.SprintUpdateId,
                    Name = ScopeNames.SprintUpdate,
                    Description = "This scope allows to update an existing sprint"
                },
                new Scope {
                    Id = DefaultIds.Scope.SprintDeleteId,
                    Name = ScopeNames.SprintDelete,
                    Description = "This scope allows to delete an existing sprint"
                },
                // Label
                new Scope {
                    Id = DefaultIds.Scope.LabelCreateId,
                    Name = ScopeNames.LabelCreate,
                    Description = "This scope allows to create a new label"
                },
                new Scope {
                    Id = DefaultIds.Scope.LabelReadId,
                    Name = ScopeNames.LabelRead,
                    Description = "This scope allows to get labels"
                },
                new Scope {
                    Id = DefaultIds.Scope.LabelUpdateId,
                    Name = ScopeNames.LabelUpdate,
                    Description = "This scope allows to update an existing label"
                },
                new Scope {
                    Id = DefaultIds.Scope.LabelDeleteId,
                    Name = ScopeNames.LabelDelete,
                    Description = "This scope allows to delete an existing label"
                },
                // Ticket
                new Scope {
                    Id = DefaultIds.Scope.TicketCreateId,
                    Name = ScopeNames.TicketCreate,
                    Description = "This scope allows to create a new ticket"
                },
                new Scope {
                    Id = DefaultIds.Scope.TicketReadId,
                    Name = ScopeNames.TicketRead,
                    Description = "This scope allows to get tickets"
                },
                new Scope {
                    Id = DefaultIds.Scope.TicketUpdateId,
                    Name = ScopeNames.TicketUpdate,
                    Description = "This scope allows to update an existing ticket"
                },
                new Scope {
                    Id = DefaultIds.Scope.TicketDeleteId,
                    Name = ScopeNames.TicketDelete,
                    Description = "This scope allows to delete an existing ticket"
                }
            };

            foreach (var scope in scopes) {
                if (unitOfWork.GetRepository<Scope>().Exist(x => x.Id == scope.Id)) {
                    continue;
                }

                unitOfWork.GetRepository<Scope>().Add(scope);
            }

            await unitOfWork.CommitAsync();
        }

        private static async Task SeedClientsAsync(IServiceProvider serviceProvider, IConfigurationSection clientHasherSection) {
            var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

            var client = unitOfWork.GetRepository<Client>()
                .Get(x => x.Id == DefaultIds.Client.MasterId, true)
                .Include(x => x.Scopes)
                .FirstOrDefault();

            if (client == null) {
                // NOTE: Once master client is created we have to reset credentials
                string key = "3e0d108840824595b1ddc2be43ad9ddb";
                string secret = "35058341a3bc457bbc7e202f032bf94e844d763248d9420294e6bcabf6df3c5d335a1a3c060141938a86fe15c3a7ce29";
                string salt = ClientHelper.GenerateSalt();
                string pepper = clientHasherSection["Pepper"];
                int iterations = Convert.ToInt32(clientHasherSection["Iterations"]);

                client = new Client {
                    Id = DefaultIds.Client.MasterId,
                    Name = "Master",
                    IsActive = true,
                    Key = key,
                    SecretSalt = salt,
                    SecretHash = ClientHelper.ComputeHash(secret, salt, pepper, iterations)
                };

                unitOfWork.GetRepository<Client>().Add(client);
            }

            if (client.Scopes == null) {
                client.Scopes = new List<ClientScope>();
            }

            var scopeIds = new List<Guid> {
                DefaultIds.Scope.ScopeReadId,
                DefaultIds.Scope.RoleReadId,
                DefaultIds.Scope.ClientCreateId,
                DefaultIds.Scope.ClientReadId,
                DefaultIds.Scope.ClientUpdateId,
                DefaultIds.Scope.ClientDeleteId,
                DefaultIds.Scope.ClientResetId,
                DefaultIds.Scope.UserCreateId,
                DefaultIds.Scope.UserReadId,
                DefaultIds.Scope.UserUpdateId,
                DefaultIds.Scope.UserDeleteId,
                DefaultIds.Scope.SprintCreateId,
                DefaultIds.Scope.SprintReadId,
                DefaultIds.Scope.SprintUpdateId,
                DefaultIds.Scope.SprintDeleteId,
                DefaultIds.Scope.LabelCreateId,
                DefaultIds.Scope.LabelReadId,
                DefaultIds.Scope.LabelUpdateId,
                DefaultIds.Scope.LabelDeleteId,
                DefaultIds.Scope.TicketCreateId,
                DefaultIds.Scope.TicketReadId,
                DefaultIds.Scope.TicketUpdateId,
                DefaultIds.Scope.TicketDeleteId
            };

            foreach (var scopeId in scopeIds) {
                if (client.Scopes.Any(x => x.ScopeId == scopeId)) {
                    continue;
                }

                client.Scopes.Add(new ClientScope { ScopeId = scopeId });
            }

            await unitOfWork.CommitAsync();
        }
    }
}