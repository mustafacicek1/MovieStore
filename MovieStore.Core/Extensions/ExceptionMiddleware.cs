using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.Extensions
{
        public class ExceptionMiddleware
        {
            private RequestDelegate _next;

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
                catch (Exception e)
                {
                    await HandleExceptionAsync(httpContext, e);
                }
            }

            private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
            {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            List<ValidationFailure> errors;
            List<string> errDetail = new List<string>();
            if (e.GetType() == typeof(ValidationException))
            {
                httpContext.Response.StatusCode = 400;
                errors = ((ValidationException)e).Errors.ToList();
                foreach (var error in errors)
                {
                    errDetail.Add(error.ErrorMessage);
                }
                var errResult = JsonConvert.SerializeObject(new { Errors = errDetail }, Formatting.None);
                return httpContext.Response.WriteAsync(errResult);
            }


            errDetail.Add(message);
            var result = JsonConvert.SerializeObject(new { Errors = errDetail }, Formatting.None);
            return httpContext.Response.WriteAsync(result);
        }
    }
}
