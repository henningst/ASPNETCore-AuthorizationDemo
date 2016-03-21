using System.Threading.Tasks;
using AuthorizationDemo.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;
using Microsoft.AspNet.Mvc;

namespace AuthorizationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authz;

        public HomeController(IAuthorizationService authz)
        {
            _authz = authz;
        }

        public async Task<IActionResult> Index()
        {
            // Create a document owned by bob
            var document = new Document() { Name = "My document name", Owner = "bob"};

            // Check if the current user is authorized by using the CustomAuthorizationHandler
            if (!await _authz.AuthorizeAsync(User, document, new OperationAuthorizationRequirement() {Name = "Read"}))
            {
                return new ChallengeResult();
            }

            return View(document);
        }
    }
}
