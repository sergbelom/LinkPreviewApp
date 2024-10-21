using Serilog;

namespace LinkPreviewApp.Services
{
    /// <summary>
    /// Logging service
    /// </summary>
    public class AppLogService : IDisposable
    {
        private Serilog.Core.Logger _logger;

        public AppLogService()
        {
            _logger = new LoggerConfiguration().CreateLogger();
            //possible improvment: use text file for logging
        }

        public void Dispose()
        {
            _logger?.Dispose();
            _logger = null;
        }

        /// <summary>
        /// Log Inforamtion message
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            _logger?.Information(message);
        }

        /// <summary>
        /// Log Error message
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            _logger?.Error(message);
        }

        /// <summary>
        /// Log Warning message
        /// </summary>
        /// <param name="message"></param>
        public void LogWarning(string message)
        {
            _logger?.Warning(message);
        }
    }
}
