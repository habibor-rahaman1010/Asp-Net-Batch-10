namespace DevSkill.Inventory.Application.MetricsServiceInterface
{
    public interface IMetricsService
    {
        public void IncrementRequestCount(string endpoint, string method);
        public void RecordResponseTime(string endpoint, double milliseconds);
        public void IncrementErrorCount(string endpoint, int statusCode);
    }
}
