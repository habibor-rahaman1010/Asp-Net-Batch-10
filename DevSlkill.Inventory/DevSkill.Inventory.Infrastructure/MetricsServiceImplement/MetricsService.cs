using DevSkill.Inventory.Application.MetricsServiceInterface;
using System.Diagnostics.Metrics;

namespace DevSkill.Inventory.Infrastructure.MetricsServiceImplement
{
    public class MetricsService : IMetricsService
    {
        public const string MeterName = "MyApp.Metrics";

        private readonly Counter<long> _requestCounter;
        private readonly Histogram<double> _responseTime;
        private readonly Counter<long> _errorCounter;

        public MetricsService(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(MeterName);

            _requestCounter = meter.CreateCounter<long>("app_requests_total", "requests", "Total number of HTTP requests");

            _responseTime = meter.CreateHistogram<double>("app_response_time_ms", "ms", "Response time in milliseconds");

            _errorCounter = meter.CreateCounter<long>("app_errors_total", "errors", "Total number of errors");
        }

        public void IncrementRequestCount(string endpoint, string method) =>
            _requestCounter.Add(1, new("endpoint", endpoint), new("method", method));

        public void RecordResponseTime(string endpoint, double ms) =>
            _responseTime.Record(ms, new KeyValuePair<string, object?>("endpoint", endpoint));

        public void IncrementErrorCount(string endpoint, int statusCode) =>
            _errorCounter.Add(1, new("endpoint", endpoint), new("status_code", statusCode));
    }
}
