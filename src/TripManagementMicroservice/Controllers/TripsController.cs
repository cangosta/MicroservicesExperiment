using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Experiments.DomainServices;

namespace TripManagementMicroservice.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {

        protected readonly ITripsDomainService tripsDomainService = null;

        public TripsController(
            ITripsDomainService tripsDomainService)
        {
            if (tripsDomainService == null) throw new ArgumentNullException("tripsDomainService");
            this.tripsDomainService = tripsDomainService;
        }

        // GET: api/trips
        [HttpGet]
        public IEnumerable<Trip> Get()
        {
            return this.tripsDomainService.GetTrips();
        }
    }
}
