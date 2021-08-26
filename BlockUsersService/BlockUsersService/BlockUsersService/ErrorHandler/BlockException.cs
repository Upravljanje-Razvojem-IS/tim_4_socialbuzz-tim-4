using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BlockUsersService.ErrorHandler
{
    [Serializable]
    public class BlockException : Exception
    {
        public BlockException() { }

        public BlockException(string mess) : base(mess) { }

        public BlockException(string mess, Exception innerException) : base(mess, innerException) { }

        protected BlockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
