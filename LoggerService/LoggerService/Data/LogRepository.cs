using LoggerService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggerService.Data
{
    public class LogRepository : ILogRepository
    {
        private readonly LoggerDBContext context;

        public LogRepository(LoggerDBContext context)
        {
            this.context = context;
        }

        public List<Log> GetLogs()
        {
            return context.Logs.ToList();
        }

        public Log Insert(Log log)
        {
            context.Add(log);
            context.SaveChanges();
            return log;
        }
    }
}
