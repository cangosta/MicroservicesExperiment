using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class UsersDomainService : IUsersDomainService
    {
        protected static IEnumerable<User> users = new List<User>() {
            new User { Id = Guid.NewGuid(), Name = "Luiz Santos", Phone = "936598289", IsActive = true },
            new User { Id = Guid.NewGuid(), Name = "Maria João", Phone = "964581822", IsActive = true },
            new User { Id = Guid.NewGuid(), Name = "Teo Pereira", Phone = "934581823" }
        };

        User IUsersDomainService.GetUserById(Guid id)
        {
            return users.FirstOrDefault(e => e.Id == id);
        }

        IEnumerable<User> IUsersDomainService.GetUsers()
        {
            return users;
        }
    }
}
