using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Infrastructure.InventoryIdentity.Requirements
{
    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "Age" && int.Parse(x.Value) > 15 && int.Parse(x.Value) < 30))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
