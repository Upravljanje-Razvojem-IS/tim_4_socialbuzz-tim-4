using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BlockUsersService.ErrorHandler
{
    [Serializable]
    public class UnblockException : Exception
    {
        public UnblockException() { }

        public UnblockException(string mess) : base(mess) { }

        public UnblockException(string mess, Exception innerException) : base(mess, innerException) { }

        protected UnblockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
