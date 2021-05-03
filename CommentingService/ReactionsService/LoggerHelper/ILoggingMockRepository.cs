using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.LoggerHelper
{
    public interface ILoggingMockRepository<T>
    {
        public void LogError(Exception ex, string message, params object[] args);

        public void LogInformation(string message);
    }
}
