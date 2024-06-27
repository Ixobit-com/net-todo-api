using NLog.Web;
using Todo.Common.Logging;

namespace Todo.API.Configurations {
    public static class LoggingConfiguration {
        public static void ConfigureLogging(this IServiceCollection services, ILoggingBuilder loggingBuilder, ConfigureHostBuilder host) {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);

            host.UseNLog();

            services.AddTransient<Logger>();
        }
    }
}