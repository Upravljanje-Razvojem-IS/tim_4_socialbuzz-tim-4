using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Models
{
    public class LogDto
    {
        public string Id { get; set; }

        public string LogLevel { get; set; }

        public string Message { get; set; }

        public string Microservice { get; set; }

        public string TimeOfAction { get; set; }
    }
}
