using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BlockUsersService.ErrorHandler
{
    [Serializable]
    public class UserException : Exception
    {
        public UserException() { }

        public UserException(string mess) : base(mess) { }

        public UserException(string mess, Exception innerException):base(mess, innerException) { }

        protected UserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
