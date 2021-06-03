using Microsoft.Extensions.Logging;
using System;

namespace Logistics.API.MockLogger
{
    public class FakeLogger : IFakeLogger
    {
        private readonly ILogger<FakeLogger> _logger;

        public FakeLogger(ILogger<FakeLogger> logger)
        {
            _logger = logger;
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }
    }
}


