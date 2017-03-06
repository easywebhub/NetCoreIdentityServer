using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.EntityFramework.Interfaces;
using id.application.Managers;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcApi.Controllers
{
    public class ClientController : BaseApiController
    {
        private IAccountManager _accountManager;
        IConfigurationDbContext _dbContext;

        public ClientController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        // GET: api/values
        [Route("clients")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_accountManager.GetListClient());
        }

        [Route("test")]
        [HttpGet]
        public IActionResult TestMiddleware()
        {
            return base.BadRequest();
            //return Ok();
        }



    }
}
