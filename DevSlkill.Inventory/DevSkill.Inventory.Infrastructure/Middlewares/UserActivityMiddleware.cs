using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DevSkill.Inventory.Infrastructure.Middlewares
{
    public class UserActivityMiddleware
    {
        private readonly RequestDelegate _next;

        public UserActivityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            InventoryDbContext dbContext)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (claim != null && Guid.TryParse(claim.Value, out Guid userId))
                {
                    var controller = context.Request.RouteValues["controller"]?.ToString();

                    var action = context.Request.RouteValues["action"]?.ToString();

                    // MVC Request না হলে Skip
                    if (!string.IsNullOrWhiteSpace(controller) && !string.IsNullOrWhiteSpace(action))
                    {
                        var today = DateTime.Today;

                        var currentUrl = context.Request.Path.ToString();

                        var ip = context.Connection.RemoteIpAddress?.ToString();

                        var browser = context.Request.Headers.UserAgent.ToString();

                        // Daily Activity Record
                        var activity = await dbContext.UserActivities
                            .FirstOrDefaultAsync(x => x.UserId == userId && x.VisitDate.Date == today);

                        if (activity == null)
                        {
                            activity = new UserActivity
                            {
                                Id = Guid.NewGuid(),
                                UserId = userId,
                                VisitDate = DateTime.Now,
                                LoginTime = DateTime.Now,
                                LastActivityTime = DateTime.Now,
                                ControllerName = controller,
                                ActionName = action,
                                PageVisited = 0,
                                ActionCount = 0,
                                Browser = browser,
                                IpAddress = ip
                            };

                            dbContext.UserActivities.Add(activity);
                        }

                        // Last Activity সবসময় Update হবে
                        activity.LastActivityTime = DateTime.Now;
                        activity.ControllerName = controller;
                        activity.ActionName = action;
                        activity.Browser = browser;
                        activity.IpAddress = ip;

                        // একই URL আজকে আগে Visit করেছে কি না
                        var alreadyVisited = await dbContext.UserActivityLogs
                                .AnyAsync(x =>
                                    x.UserId == userId &&
                                    x.Url == currentUrl &&
                                    x.VisitTime.Date == today);

                        // Unique Visit হলে Count বাড়বে
                        if (!alreadyVisited)
                        {
                            activity.PageVisited++;
                            activity.ActionCount++;

                            dbContext.UserActivityLogs.Add(
                                new UserActivityLog
                                {
                                    Id = Guid.NewGuid(),
                                    UserId = userId,
                                    ControllerName = controller,
                                    ActionName = action,
                                    Url = currentUrl,
                                    HttpMethod = context.Request.Method,
                                    VisitTime = DateTime.Now,
                                    Browser = browser,
                                    IpAddress = ip
                                });
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            await _next(context);
        }
    }
}