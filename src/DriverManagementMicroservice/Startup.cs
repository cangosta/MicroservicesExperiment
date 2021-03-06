﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Experiments.DomainServices;


namespace DriverManagementMicroservice
{
    public partial class Startup
    {

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IDriversDomainService, DriversDomainService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            StartEventListeners(app);
            app.UseMvc();
        }
    }
}
