using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Todo.Common.Constants;

namespace Todo.Common.Logging {
    public class Logger {
        private const string ScopePropertyName = "scope";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<Logger> _logger;

        public Logger(
            IHttpContextAccessor httpContextAccessor,
            ILogger<Logger> logger) {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public void Info(string message) {
            Log(LogLevel.Information, null, message);
        }

        public void Info(string message, string? scope) {
            Log(LogLevel.Information, null, message, scope);
        }

        public void Info(Exception exception, string message, string? scope) {
            Log(LogLevel.Information, exception, message, scope);
        }

        public void Error(string message) {
            Log(LogLevel.Error, null, message);
        }

        public void Error(string message, string? scope) {
            Log(LogLevel.Error, null, message, scope);
        }

        public void Error(Exception exception, string message) {
            Log(LogLevel.Error, exception, message);
        }

        public void Error(Exception exception, string message, string? scope) {
            Log(LogLevel.Error, exception, message, scope);
        }

        private void Log(LogLevel level, Exception? exception, string message) {
            Log(level, exception, message, GetCurrentLogScope());
        }

        private void Log(LogLevel level, Exception? exception, string message, string? scope) {
            scope = String.IsNullOrEmpty(scope) ? null : $"_{scope}";

            using (NLog.ScopeContext.PushProperty(ScopePropertyName, scope)) {
                _logger.Log(level, exception, message);
            }
        }

        private string GetCurrentLogScope() {
            string? userEmail = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            if (!String.IsNullOrEmpty(userEmail)) {
                return $"user_{userEmail}";
            }

            string? clientName = _httpContextAccessor?.HttpContext?.User?.FindFirst(TodoClaimTypes.ClientName)?.Value;
            if (!String.IsNullOrEmpty(clientName)) {
                return $"client_{clientName}";
            }

            return null;
        }
    }
}