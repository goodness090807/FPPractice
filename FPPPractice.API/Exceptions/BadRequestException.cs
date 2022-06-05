using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FPPPractice.API.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string errorMessage = "") : base(HttpStatusCode.BadRequest, errorMessage)
        {

        }
    }
}
