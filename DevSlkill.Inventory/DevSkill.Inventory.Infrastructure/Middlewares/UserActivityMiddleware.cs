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

                    // MVC request না হলে skip
                    if (!string.IsNullOrEmpty(controller))
                    {
                        var today = DateTime.Today;

                        var ip = context.Connection.RemoteIpAddress?.ToString();

                        var browser =
                            context.Request.Headers.UserAgent.ToString();

                        // Daily Summary
                        var activity = await dbContext.UserActivities.FirstOrDefaultAsync(x => x.UserId == userId && x.VisitDate.Date == today);

                        if (activity == null)
                        {
                            activity = new UserActivity
                            {
                                Id = Guid.NewGuid(),
                                UserId = userId,
                                ControllerName = controller,
                                ActionName = action,
                                VisitDate = DateTime.Now,
                                LoginTime = DateTime.Now,
                                LastActivityTime = DateTime.Now,
                                PageVisited = 1,
                                ActionCount = 1,
                                Browser = browser,
                                IpAddress = ip
                            };

                            dbContext.UserActivities.Add(activity);
                        }
                        else
                        {
                            activity.ControllerName = controller;
                            activity.ActionName = action;
                            activity.PageVisited++;
                            activity.ActionCount++;
                            activity.LastActivityTime = DateTime.Now;
                            activity.Browser = browser;
                            activity.IpAddress = ip;
                        }

                        // Detailed Log
                        dbContext.UserActivityLogs.Add(
                            new UserActivityLog
                            {
                                Id = Guid.NewGuid(),
                                UserId = userId,
                                ControllerName = controller,
                                ActionName = action,
                                Url = context.Request.Path,
                                HttpMethod = context.Request.Method,
                                VisitTime = DateTime.Now,
                                Browser = browser,
                                IpAddress = ip
                            });

                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            await _next(context);
        }
    }
}
