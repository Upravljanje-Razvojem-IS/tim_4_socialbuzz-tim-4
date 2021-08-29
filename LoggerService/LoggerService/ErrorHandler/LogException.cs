using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;


namespace LoggerService.ErrorHandler
{
    [Serializable]
    public class LogException : Exception
    {
        public LogException()
        {

        }

        public LogException(string message) : base(message)
        {

        }

        public LogException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected LogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
