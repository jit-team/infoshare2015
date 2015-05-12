using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace Infoshare
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseFileServer();
            
            app.UseSignalR();
        }
    }
}
