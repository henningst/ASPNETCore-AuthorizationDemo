using System.Security.Claims;
using AuthorizationDemo.Authz;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Make sure our CustomAuthorizationHandler is injected
            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();

            services.AddMvc(options =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseIISPlatformHandler();

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
            });

            // Automatically set auth cookie on the first request.
            // The user should be authorized on the second request.
            app.Use(async (context, next) =>
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "bob") }, CookieAuthenticationDefaults.AuthenticationScheme));
                await context.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                await next.Invoke();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
