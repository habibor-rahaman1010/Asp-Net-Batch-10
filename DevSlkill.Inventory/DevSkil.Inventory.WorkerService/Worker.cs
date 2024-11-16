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
                await DeleteOldApplicationLogsAsync();
                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }

        /*This code deletes logs from the database that are older than 7 days every 24 hours.
        I will ensure it works continuously as long as the application is running.*/
        
        private async Task DeleteOldApplicationLogsAsync()
        {
            try
            {
                var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
                var oldLogs = _inventoryDbContext.ApplicationLogs
                    .Where(x => x.TimeStamp < oneWeekAgo);

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
