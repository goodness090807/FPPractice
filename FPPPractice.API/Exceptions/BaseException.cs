using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FPPPractice.API.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(HttpStatusCode statusCode, string errorMessage) : base(errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
