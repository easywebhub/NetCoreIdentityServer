using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServerWithAspNetIdentity.Controllers
{
    public class ManageApiResource : Controller
    {
        IConfigurationDbContext _dbContext;
        public ManageApiResource(IConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            _dbContext.ApiResources.Add(new IdentityServer4.EntityFramework.Entities.ApiResource() { Name = "Abc", DisplayName = "Abc" });
            _dbContext.SaveChanges();
            var apiResources = _dbContext.ApiResources.ToList();
            return View(apiResources);
        }
    }
}
