using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    public interface IDriversDomainService
    {
        IEnumerable<Driver> GetDrivers();
        Driver GetDrivers(Guid id);
        Driver CreateDriver(Driver driver);
        Driver GetNextDriver();
    }
}
