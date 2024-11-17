using DevSkill.Inventory.Infrastructure.Data;
using DevSkill.Inventory.Infrastructure.InventoryIdentity;
using DevSkill.Inventory.Infrastructure.InventoryIdentity.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            //This service for Application Identity user
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            
            
            //add policy role configuration
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomAdminAccess", policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireRole("Support");
                    policy.RequireRole("Member");
                });

                options.AddPolicy("CustomAccess", policy =>
                {
                    policy.RequireRole("Member");
                    policy.RequireRole("Support");
                });
            });

            //add Claim base authentication configuration
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ReadPermission", policy =>
                {
                    policy.RequireClaim("Read", "true");
                });

                options.AddPolicy("CreatePermission", policy =>
                {
                    policy.RequireClaim("Create", "true");
                });

                options.AddPolicy("UpdatePermission", policy =>
                {
                    policy.RequireClaim("Update", "true");
                });

                options.AddPolicy("DeletePermission", policy =>
                {
                    policy.RequireClaim("Delete", "true");
                });
            });

            
            //add requirement base authentication configuration
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AgeRestriction", policy =>
                {
                    policy.Requirements.Add(new AgeRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, AgeRequirementHandler>();
        }

        // Extension method for seeding admin roles and user
        public static async Task SeedAdminUserAndRolesAsync(this IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string[] rolesName = { "Admin", "Support", "Member" };
            string adminEmail = "habibor.rahaman1010@gmail.com";
            string adminPassword = "c++c++c#"; // Make sure to use a strong password

            // Ensure roles exist
            foreach (var roleName in rolesName)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole() { Name = roleName});   
                }
            }

            // Create admin user if it doesn't exist
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                await userManager.CreateAsync(adminUser, adminPassword);

                // Assign roles to the admin user
                foreach (var roleName in rolesName)
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);
                }
            }
        }


    }
}
