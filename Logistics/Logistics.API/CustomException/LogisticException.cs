using System;

namespace Logistics.API.CustomException
{
    public class LogisticException : Exception
    {
        public int StatusCode { get; set; }
        public LogisticException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
