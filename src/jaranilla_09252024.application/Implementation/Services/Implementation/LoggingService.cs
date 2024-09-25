using jaranilla_09252024.application.Implementation.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaranilla_09252024.application.Implementation.Services.Implementation
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public Task LogErrorAsync(string message, Exception exception)
        {
            _logger.LogError(exception, message);
            return Task.CompletedTask;
        }

        public Task LogInformationAsync(string message)
        {
            _logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}
