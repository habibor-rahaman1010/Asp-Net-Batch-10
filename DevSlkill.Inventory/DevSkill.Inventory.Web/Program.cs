using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevSkill.Inventory.Web.Data;
using DevSkill.Inventory.Web.WebModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace DevSkill.Inventory.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigurationBuilder configuration = new ConfigurationBuilder();
            IConfigurationBuilder configurationBuilder = configuration.SetBasePath(Directory.GetCurrentDirectory());
            IConfigurationBuilder configurationBuilder1 = configurationBuilder.AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = configurationBuilder1.Build();

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configurationRoot).CreateBootstrapLogger();
           

            try
            {
                Log.Information("Application Starting...");
                var builder = WebApplication.CreateBuilder(args);

                IHostBuilder hostBuilder = builder.Host.UseSerilog((ctx, lc) =>
                    lc.MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(builder.Configuration)
                );

                // Add services to the container.
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
                builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
                {
                    containerBuilder.RegisterModule(new WebModule());
                });


                builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();
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

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();

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

