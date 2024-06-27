using System.Security.Claims;
using Todo.API.Constants;
using Todo.Common.Constants;

namespace Todo.API.Configurations {
    public static class AuthorizationConfiguration {
        public static void ConfigureAuthorization(this IServiceCollection services) {
            services
                .AddAuthorization(options => {
                    // Authorization
                    options.AddPolicy(PolicyNames.AuthorizedAsUser, policy => {
                        policy
                            .RequireClaim(ClaimTypes.NameIdentifier);
                    });

                    // Scope
                    options.AddPolicy(PolicyNames.ScopeRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.ScopeRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });

                    // Role
                    options.AddPolicy(PolicyNames.RoleRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.RoleRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });

                    // Client
                    options.AddPolicy(PolicyNames.ClientCreate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.ClientCreate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.ClientRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.ClientRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.ClientUpdate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.ClientUpdate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.ClientDelete, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.ClientDelete)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.ClientReset, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.ClientReset)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });

                    // User
                    options.AddPolicy(PolicyNames.UserCreate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.UserCreate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.UserRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.UserRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.UserUpdate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.UserUpdate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.UserDelete, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.UserDelete)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });

                    // Email template
                    options.AddPolicy(PolicyNames.EmailTemplateRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.EmailTemplateRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.EmailTemplateUpdate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.EmailTemplateUpdate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });
                    options.AddPolicy(PolicyNames.EmailTemplatePreview, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.EmailTemplatePreview)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator);
                    });

                    // Sprint
                    options.AddPolicy(PolicyNames.SprintCreate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.SprintCreate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.SprintRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.SprintRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.SprintUpdate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.SprintUpdate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.SprintDelete, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.SprintDelete)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });

                    // Label
                    options.AddPolicy(PolicyNames.LabelCreate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.LabelCreate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.LabelRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.LabelRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.LabelUpdate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.LabelUpdate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.LabelDelete, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.LabelDelete)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });

                    // Ticket
                    options.AddPolicy(PolicyNames.TicketCreate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.TicketCreate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.TicketRead, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.TicketRead)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.TicketUpdate, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.TicketUpdate)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                    options.AddPolicy(PolicyNames.TicketDelete, policy => {
                        policy
                            .RequireClaim(TodoClaimTypes.ClientScope, ScopeNames.TicketDelete)
                            .RequireClaim(ClaimTypes.Role, RoleNames.Administrator, RoleNames.User);
                    });
                });
        }
    }
}