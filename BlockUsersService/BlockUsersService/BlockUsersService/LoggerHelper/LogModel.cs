using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockUsersService.LoggerHelper
{
    public class LogModel
    {
        public Guid Id { get; set; }
        public string LogLevel { get; set; }
        public string Microservice { get; set; }
        public string Message { get; set; }
        public string TimeOfAction { get; set; }
    }
}
