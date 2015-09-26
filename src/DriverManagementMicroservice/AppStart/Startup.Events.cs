using DriverManagementMicroservice.Subscribers;
using Experiments.DomainServices;
using Microsoft.AspNet.Builder;
using System;
using System.Threading.Tasks;
using Microsoft.Framework.DependencyInjection;

namespace DriverManagementMicroservice
{
    public partial class Startup
    {
        public void StartEventListeners(IApplicationBuilder app)
        {
            Task.Run(
                () =>
                {
                    Console.WriteLine(" [*] Starting up application listeners...");

                    // starts the queue listener
                    Console.WriteLine(" [*] Starting up trips listener...");
                    var tripEventsSubscriber = new TripsSubscriber(
                        app.ApplicationServices.GetService<IDriversDomainService>());
                    tripEventsSubscriber.Listen();

                    Console.ReadLine();
                });
        }
    }
}
