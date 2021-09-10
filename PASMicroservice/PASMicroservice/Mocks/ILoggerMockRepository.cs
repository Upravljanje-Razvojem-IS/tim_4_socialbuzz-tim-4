using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PASMicroservice.Mocks
{
    public interface ILoggerMockRepository<T>
    {
        void LogInformation(string message);
        void LogError(Exception ex, string message, params object[] args);
    }
}
