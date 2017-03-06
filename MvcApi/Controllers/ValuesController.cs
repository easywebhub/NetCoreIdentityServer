using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.EntityFramework.Interfaces;
using id.data;

namespace MvcApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IConfigurationDbContext _dbContext;
        ApplicationDbContext _db;

        public ValuesController(IConfigurationDbContext dbContext, ApplicationDbContext db)
        {
            _dbContext = dbContext;
            _db = db;
        }

        // GET: api/values
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            //return new ObjectResult(_dbContext.Clients.ToList());
            return new ObjectResult(_db.Users.ToList());
        }

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
