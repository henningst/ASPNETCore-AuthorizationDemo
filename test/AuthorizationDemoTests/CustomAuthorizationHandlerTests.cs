using System.Collections.Generic;
using System.Security.Claims;
using AuthorizationDemo.Authz;
using AuthorizationDemo.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;
using Xunit;

namespace AuthorizationDemoTests
{
    public class CustomAuthorizationHandlerTests
    {
        [Fact]
        public void Handle_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = new Document() {Name = "Homer's document", Owner = "homer.simpson"};
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, "homer.simpson") }));
            var requirement = new OperationAuthorizationRequirement {Name = "Read"};

            var authzContext = new AuthorizationContext(new List<IAuthorizationRequirement> { requirement }, user, resource);

            var authzHandler = new CustomAuthorizationHandler();
            authzHandler.Handle(authzContext);
            
            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public void Handle_WhenCalledWithIllegalUser_ShouldNotSucceed()
        {
            var resource = new Document() { Name = "Homer's document", Owner = "homer.simpson" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Name, "ned.flanders") }));
            var requirement = new OperationAuthorizationRequirement { Name = "Read" };

            var authzContext = new AuthorizationContext(new List<IAuthorizationRequirement> { requirement }, user, resource);

            var authzHandler = new CustomAuthorizationHandler();
            authzHandler.Handle(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }
    }
}