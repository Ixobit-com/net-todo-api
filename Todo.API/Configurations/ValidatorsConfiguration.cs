using FluentValidation;
using Todo.API.Models;
using Todo.API.Validators;

namespace Todo.API.Configurations {
    public static class ValidatorsConfiguration {
        public static void ConfigureValidators(this IServiceCollection services) {
            // Authorization
            services.AddScoped<IValidator<AuthModel>, AuthValidator>();

            // Client
            services.AddScoped<IValidator<ClientCreateModel>, ClientCreateValidator>();
            services.AddScoped<IValidator<ClientUpdateModel>, ClientUpdateValidator>();

            // User
            services.AddScoped<IValidator<UserCreateModel>, UserCreateValidator>();
            services.AddScoped<IValidator<UserUpdateModel>, UserUpdateValidator>();

            // Profile
            services.AddScoped<IValidator<ProfileUpdateModel>, ProfileUpdateValidator>();
            services.AddScoped<IValidator<ForgotPasswordModel>, ForgotPasswordValidator>();
            services.AddScoped<IValidator<ResetPasswordModel>, ResetPasswordValidator>();
            services.AddScoped<IValidator<ChangePasswordModel>, ChangePasswordValidator>();

            // Label
            services.AddScoped<IValidator<LabelCreateModel>, LabelCreateValidator>();
            services.AddScoped<IValidator<LabelUpdateModel>, LabelUpdateValidator>();

            // Sprint
            services.AddScoped<IValidator<SprintCreateModel>, SprintCreateValidator>();
            services.AddScoped<IValidator<SprintUpdateModel>, SprintUpdateValidator>();

            // Ticket
            services.AddScoped<IValidator<TicketCreateModel>, TicketCreateValidator>();
            services.AddScoped<IValidator<TicketUpdateModel>, TicketUpdateValidator>();
        }
    }
}