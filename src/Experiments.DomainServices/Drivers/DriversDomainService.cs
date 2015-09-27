using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    public class DriversDomainService : IDriversDomainService
    {
        private int nextDriver;
        private List<Driver> drivers = null;

        public DriversDomainService()
        {
            nextDriver = 0;
            drivers = new List<Driver>();
            drivers.Add(new Driver() {
                Id = Guid.Parse("6b89b8be-f5b9-476a-ae48-9fbda8606652"),
                Name = "Luiz Alberto de Castro Santos"
            });
        }

        public Driver CreateDriver(Driver driver)
        {
            driver.Id = Guid.NewGuid();
            this.drivers.Add(driver);
            return driver;
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return this.drivers;
        }

        public Driver GetDrivers(Guid id)
        {
            return this.drivers.FirstOrDefault(d => d.Id == id);
        }

        public Driver GetNextDriver()
        {
            var driver = this.drivers.ElementAt(this.nextDriver);
            this.nextDriver = (this.nextDriver + 1) % drivers.Count;
            return driver;
        }

    }
}
