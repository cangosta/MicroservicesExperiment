using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace NotificationsMicroservice
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // add SignalR to the pipeline
            app.Map("/signalr", map =>
            {
                map.UseCors(
                    policies =>
                    {
                        policies.AllowAnyOrigin();
                        policies.AllowCredentials();
                        policies.AllowAnyHeader();
                        policies.AllowAnyMethod();
                    });
                map.RunSignalR();
            });

            // Add MVC to the request pipeline.
            app.UseMvc();
        }
    }
}
