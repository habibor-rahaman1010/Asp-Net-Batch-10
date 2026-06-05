using DevSkill.Inventory.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DevSkill.Inventory.Infrastructure.Extensions
{
    public static class ApplicationMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserActivityTracking(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserActivityMiddleware>();
        }
    }
}
