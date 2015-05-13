using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace Infoshare
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
           
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseRuntimeInfoPage();
            
            app.UseErrorPage();

            app.UseFileServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "areaRoute",
                 template: "{area:exists}/{controller}/{action}",
                 defaults: new { action = "Index" });
                routes.MapRoute(
                 name: "default",
                 template: "{controller}/{action}/{id?}",
                 defaults: new { controller = "Home", action = "Index" });
                routes.MapRoute(
                 name: "api",
                 template: "{controller}/{id?}");
            });
        }
    }
}
