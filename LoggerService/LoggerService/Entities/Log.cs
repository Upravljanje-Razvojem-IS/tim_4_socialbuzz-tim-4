using System;
using System.Collections.Generic;

#nullable disable

namespace LoggerService.Entities
{
    public partial class Log
    {
        public Guid Id { get; set; }
        public string LogLevel { get; set; }
        public string RequestId { get; set; }
        public string Microservice { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string TimeOfAction { get; set; }
    }
}
