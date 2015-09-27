using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;

namespace NotificationsMicroservice
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            // add SignalR to the pipeline
            app.Map("/signalr", map => {
                map.UserCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                map.RunSignalR(hubConfiguration);
            });

            // Add MVC to the request pipeline.
            app.UseMvc();
        }
    }
}
