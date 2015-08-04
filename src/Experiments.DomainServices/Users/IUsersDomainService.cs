using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Experiments.DomainServices
{
    public interface IUsersDomainService 
    {
        User GetUserById(Guid id);
    }
}
