using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationsMicroservice.Hubs
{
    public class NotificationsHub : Hub
    {
        public void JoinGroup(string groupName)
        {
            this.Groups.Add(Context.ConnectionId, groupName);
        }

        public void LeaveGroup(string groupName)
        {
            this.Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}
