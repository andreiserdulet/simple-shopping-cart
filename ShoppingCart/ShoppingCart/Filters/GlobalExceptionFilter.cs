using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShoppingCart.Filters
{

    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var guid = Guid.NewGuid().ToString();
            _logger.LogError(context.Exception, guid);
            context.ExceptionHandled = true;

            if (context.Exception is InvalidParameterException)
            {
                var ex = (InvalidParameterException)context.Exception;
                var response = new ErrorDto
                {
                    Errors = new List<string> { ex.Message }
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = 400
                };
            }
            else
            {
                var response = new ErrorDto
                {
                    Errors = new List<string> { "Something went wrong. Please contact the support team. Id: " + guid }
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }

    public class ErrorDto
    {
        public List<string> Errors { get; set; }
    }
}
