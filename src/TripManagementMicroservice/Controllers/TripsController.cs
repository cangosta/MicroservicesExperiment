using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Experiments.DomainServices;
using RestSharp;
using TripManagementMicroservice.Publishers;

namespace TripManagementMicroservice.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {

        #region properties
        protected readonly ITripsDomainService tripsDomainService = null;
        protected readonly ITripPublisher tripPublisher = null;
        #endregion

        #region constructor
        public TripsController(
            ITripsDomainService tripsDomainService,
            ITripPublisher tripPublisher)
        {
            if (tripsDomainService == null) throw new ArgumentNullException("tripsDomainService");
            if (tripPublisher == null) throw new ArgumentNullException("tripPublisher");
            this.tripPublisher = tripPublisher;
            this.tripsDomainService = tripsDomainService;
        }
        #endregion

        #region actions

        // GET: api/trips
        [HttpGet]
        public IEnumerable<Trip> Get()
        {
            return this.tripsDomainService.GetTrips();
        }

        // POST: api/trips
        [HttpPost]
        public Trip Post([FromBody]Trip newTrip)
        {
            // check user existence
            User passenger = null;

            // get user information
            var response = getPassenger(newTrip.UserId, ref passenger);

            if (response == System.Net.HttpStatusCode.OK && passenger.IsActive)
            {
                
                var trip = this.tripsDomainService.CreateTrip(newTrip.Destination, passenger);

                // publish the creation of the trip
                this.tripPublisher.notifyNewTrip(trip);

                return trip;
                
            }

            throw new InvalidOperationException();
        }

        #endregion

        #region private

        private System.Net.HttpStatusCode getPassenger(Guid id, ref User passenger)
        {
            // TODO extract this method

            // TODO move URI to a configuration file
            var userManagementClient = new RestClient("http://localhost:5000");

            var request = new RestRequest("api/users/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());

            var response = userManagementClient.Execute<User>(request);

            // set the passenger
            passenger = response.Data;

            return response.StatusCode;
        }

        #endregion
    }
}
