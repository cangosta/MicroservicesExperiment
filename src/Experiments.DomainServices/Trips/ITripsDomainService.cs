using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    public interface ITripsDomainService
    {
        IEnumerable<Trip> GetTrips();
        Trip CreateTrip(string destination, Guid userId);
    }
}
