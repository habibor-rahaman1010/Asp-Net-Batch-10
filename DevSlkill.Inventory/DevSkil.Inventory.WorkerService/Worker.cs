using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevSkil.Inventory.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly InventoryDbContext _inventoryDbContext;

        public Worker(ILogger<Worker> logger, InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(5000, stoppingToken);
                await DeleteOldApplicationLogsAsync();
            }
        }

        private async Task DeleteOldApplicationLogsAsync()
        {
            try
            {
                //var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
                var oldLogs = _inventoryDbContext.ApplicationLogs;
                _inventoryDbContext.ApplicationLogs.RemoveRange(oldLogs);
                await _inventoryDbContext.SaveChangesAsync();
                _logger.LogInformation("Old logs deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting old logs.");
            }
        }
    }
}
