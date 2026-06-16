using DevSkill.Inventory.Application.MetricsServiceInterface;
using System.Diagnostics;

namespace DevSkill.Inventory.Web.Middleware
{
    public class MetricsMiddleware
    {
        private readonly RequestDelegate _next;

        public MetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMetricsService metrics)
        {
            var endpoint = context.Request.Path.Value ?? "unknown";
            var method = context.Request.Method;

            metrics.IncrementRequestCount(endpoint, method);

            var sw = Stopwatch.StartNew();
            try
            {
                await _next(context);
            }
            finally
            {
                sw.Stop();
                metrics.RecordResponseTime(endpoint, sw.Elapsed.TotalMilliseconds);

                if (context.Response.StatusCode >= 400)
                {
                    metrics.IncrementErrorCount(endpoint, context.Response.StatusCode);
                }
            }
        }
    }
}