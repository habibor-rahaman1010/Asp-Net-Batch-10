using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevSkill.Inventory.Infrastructure.Data;
using DevSkill.Inventory.Web.WebModules;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure.Extensions;


namespace DevSkill.Inventory.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigurationBuilder configuration = new ConfigurationBuilder();
            IConfigurationBuilder configurationBuilder = configuration.SetBasePath(Directory.GetCurrentDirectory());
            IConfigurationBuilder configurationBuilder1 = configurationBuilder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder1.Build();

            string? connection = configurationRoot.GetConnectionString("DefaultConnection");
            string? tableName = "ApplicationLogs";

            Log.Logger = new LoggerConfiguration().MinimumLevel
                .Debug().WriteTo.MSSqlServer(
                      connectionString: connection,
                      sinkOptions: new MSSqlServerSinkOptions { TableName = tableName, AutoCreateSqlTable = false })
                .ReadFrom.Configuration(configurationRoot).CreateBootstrapLogger();
           

            try
            {
                Log.Information("Application Starting...");
                var builder = WebApplication.CreateBuilder(args);
              
                IHostBuilder hostBuilder = builder.Host.UseSerilog((ctx, lc) =>
                    lc.MinimumLevel.Debug().WriteTo.MSSqlServer(
                        connectionString: connection,
                        sinkOptions: new MSSqlServerSinkOptions { TableName = tableName, AutoCreateSqlTable = false })
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(builder.Configuration)                 
                );
               
                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                var migrationAssembly = Assembly.GetExecutingAssembly().FullName;
                if (string.IsNullOrEmpty(migrationAssembly))
                {
                    throw new InvalidOperationException("Migration assembly not found.");
                }

                //builder.WebHost.UseUrls("http://*:80");

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));

                builder.Services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(connectionString, (x) => x.MigrationsAssembly(migrationAssembly)));

                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                //This is my extension method here have all identity related configuration...
                builder.Services.AddIdentity();            

                //This is Autofac service...
                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.RegisterModule(new WebModule(connectionString, migrationAssembly));
                });

                //This service for automapper
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                //This service for mail servecing
                builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

                builder.Services.AddControllersWithViews();


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //app.MapRazorPages();


                // Seed polyciry roles and admin user on startup
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        await services.SeedAdminUserAndRolesAsync();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }

                app.Run();
            }

            catch (Exception ex)
            {
                Log.Fatal(ex.ToString(), "Faild to start application!");
            }

            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}