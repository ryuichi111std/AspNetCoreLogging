using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreLogging.Filters
{
    public class MyExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logger;

        public MyExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Unhandled Exception");
        }

        public void OnException(ExceptionContext context)
        {
            Guid errorCode = Guid.NewGuid();
            _logger.LogError("guid: {errorCode}, exception:{@Exception}", errorCode, context.Exception);

            context.ExceptionHandled = true;
            context.HttpContext.Response.Redirect("/Home/Error/" + errorCode.ToString());
        }
    }
}
