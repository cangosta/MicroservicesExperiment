using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Experiments.DomainServices;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DriverManagementMicroservice.Controllers
{
    [Route("api/[controller]")]
    public class DriversController : Controller
    {
        protected IDriversDomainService driversDomainService = null;

        public DriversController(
            IDriversDomainService driversDomainService)
        {
            this.driversDomainService = driversDomainService;
        }

        // GET: api/drivers
        [HttpGet]
        public IEnumerable<Driver> Get()
        {
            return this.driversDomainService.GetDrivers();
        }

        // POST api/drivers
        [HttpPost]
        public void Post([FromBody]Driver driver)
        {
            this.driversDomainService.CreateDriver(driver);
        }

    }
}
