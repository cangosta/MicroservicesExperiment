using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    public class TripsDomainService : ITripsDomainService
    {
        private static List<Trip> trips = new List<Trip>();

        public Trip CreateTrip(string destination, Guid userId)
        {
            var newTrip = new Trip() {
                Id = Guid.NewGuid(),
                Destination = destination,
                UserId = userId,
                CreatedAt = DateTime.Now
            };
            trips.Add(newTrip);

            return newTrip;
        }

        public IEnumerable<Trip> GetTrips()
        {
            return trips;
        }
    }
}
