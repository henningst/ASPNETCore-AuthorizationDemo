using Microsoft.AspNet.Authorization;

namespace AuthorizationDemo.Models
{
    public class Document : IAuthorizationRequirement
    {
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}
