using System.Security.Claims;
using AuthorizationDemo.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;

namespace AuthorizationDemo.Authz
{
    public class CustomAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement,Document>
    {
        protected override void Handle(AuthorizationContext context, OperationAuthorizationRequirement requirement, Document resource)
        {
            var claim = context.User.FindFirst(ClaimTypes.Name);
            if(claim != null && claim.Value.Equals(resource.Owner))
            {
                context.Succeed(requirement);
            }
        }
    }
}
