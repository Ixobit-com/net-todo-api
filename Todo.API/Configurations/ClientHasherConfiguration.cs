using Todo.Common.Settings;

namespace Todo.API.Configurations {
    public static class ClientHasherConfiguration {
        public static void ConfigureClientHasher(this IServiceCollection services, ConfigurationManager configuration) {
            var section = configuration.GetSection("ClientHasher");

            services.Configure<ClientHasherSettings>(section);
        }
    }
}