using FPPPractice.API.Exceptions;
using FPPPractice.API.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FPPPractice.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                ErrorDetails errorDetails;
                if (ex is BaseException exception)
                {
                    httpContext.Response.StatusCode = (int)exception.StatusCode;
                    errorDetails = new ErrorDetails(httpContext.Response.StatusCode, exception.ErrorMessage);
                }
                else
                {
                    // NOTE: 這部分是系統內沒抓到的Bug，所以就直接500出去
                    // TODO: 500的錯誤可以再加個Logger
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorDetails = new ErrorDetails(httpContext.Response.StatusCode, "伺服器發生錯誤", ex.StackTrace?.ToString());

                }

                // 回傳格式
                var json = JsonConvert.SerializeObject(errorDetails);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
