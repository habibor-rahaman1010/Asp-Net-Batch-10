using Autofac;
using Serilog.Events;
using Serilog;
using Autofac.Extensions.DependencyInjection;
using DevSkill.Inventory.WorkerService.WorkerModules;


namespace DevSkill.Inventory.WorkerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Bootstrap logger: Basic configuration for early logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateBootstrapLogger();

            Log.Information("Starting application with bootstrap logger...");

            try
            {
                // Load configuration
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                // Retrieve connection string and assembly name
                var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                var migrationAssemblyName = typeof(Worker).Assembly.FullName;

                if (string.IsNullOrEmpty(migrationAssemblyName))
                {
                    throw new InvalidOperationException("Migration assembly not found.");
                }

                // Replace bootstrap logger with the fully configured logger
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                Log.Information("Application Starting up");

                IHost host = Host.CreateDefaultBuilder(args)
                    .UseWindowsService()
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .UseSerilog()
                    .ConfigureContainer<ContainerBuilder>(builder =>
                    {
                        builder.RegisterModule(new WorkerModule(connectionString, migrationAssemblyName));
                    })
                    .ConfigureServices(services =>
                    {
                        services.AddHostedService<Worker>();
                    })
                    .Build();

                await host.RunAsync();
            }

            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }

            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}