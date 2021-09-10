using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PASMicroservice.Mocks
{
    public class LoggerMockRepository<T> : ILoggerMockRepository<T>
    {
        private readonly ILogger<T> logger;

        public LoggerMockRepository(ILogger<T> logger)
        {
            this.logger = logger;
        }

        public void LogError(Exception ex, string message, params object[] args)
        {
            this.logger.LogError(ex, message, args);
        }

        public void LogInformation(string message)
        {
            this.logger.LogInformation(message);
        }
    }
}
