using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspNetCoreLogging.Filters
{
    /// <summary>
    /// パフォーマンス測定用のカスタムアクションフィルタークラスです。
    /// </summary>
    public class PerformanceLogFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private DateTime _start;

        public PerformanceLogFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("PerformanceLogFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this._start = DateTime.Now;

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            TimeSpan ts = DateTime.Now - this._start;
            if (ts.TotalMilliseconds > 5000)
            {
                EventId eventId = new EventId(1000, "PerformanceAlert");
                var emp = JsonConvert.DeserializeObject<Models.Employee>(context.HttpContext.Session.GetString("LoginEmployee"));
                this._logger.LogWarning(eventId, "{@emp}", emp);
            }

            base.OnResultExecuted(context);
        }
    }
}
