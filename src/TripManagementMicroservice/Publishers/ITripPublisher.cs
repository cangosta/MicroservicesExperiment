using Experiments.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripManagementMicroservice.Publishers
{
    public interface ITripPublisher
    {

        void notifyNewTrip(Trip newTrip);

    }
}
