using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.SignalR;
using NotificationsMicroservice.Hubs;
using Experiments.DomainServices;
using Microsoft.AspNet.SignalR.Infrastructure;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace NotificationsMicroservice.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        protected IHubContext notificationsHub = null;
        public NotificationsController(
            IConnectionManager connectionManager)
        {
            this.notificationsHub = connectionManager.GetHubContext<NotificationsHub>();
        }

        // POST api/notifications
        [HttpPost]
        public void Post([FromBody]Notification notification)
        {
            this.notificationsHub.Clients.Group(notification.GroupName).Invoke(notification.Event, notification.Data);
            Console.WriteLine("Notification sent: {0}", notification.Event);
        }
    }
}
