using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoggerService.Entities;

namespace LoggerService.Data
{
    public interface ILogRepository
    {
        public Log Insert(Log log);

        public List<Log>GetLogs();

    }
}
