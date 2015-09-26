using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    public class Notification
    {
        public Guid Id {get; set;}
        public Object Data { get; set; }
        public String GroupName { get; set; }
        public String Event { get; set; }
    }
}
