using Experiments.DomainServices;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        protected readonly IUsersDomainService usersDomainService = null;

        public UsersController(
            IUsersDomainService usersDomainService)
        {
            if (usersDomainService == null) throw new ArgumentNullException("usersDomainService");
            this.usersDomainService = usersDomainService;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return this.usersDomainService.GetUserById(id);
        }
    }
}
