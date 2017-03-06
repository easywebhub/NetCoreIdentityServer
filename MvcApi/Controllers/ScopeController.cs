using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace MvcApi.Controllers
{
    public class ScopeController: Controller
    {
        IConfigurationDbContext _dbContext;
        
        public ScopeController(IConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult AddScope()
        {
            _dbContext.Clients.Add(new IdentityServer4.EntityFramework.Entities.Client()
            {
                AllowedScopes = new List<IdentityServer4.EntityFramework.Entities.ClientScope>()
                {
                    new IdentityServer4.EntityFramework.Entities.ClientScope()
                    {
                        Scope = ""
                    }
                },
                Claims = new List<IdentityServer4.EntityFramework.Entities.ClientClaim>()
                {
                    new IdentityServer4.EntityFramework.Entities.ClientClaim()
                    {
                        Type = "",
                        Value = ""
                    }
                }
            });

            _dbContext.ApiResources.Add(new IdentityServer4.EntityFramework.Entities.ApiResource()
            {
                Scopes = new List<IdentityServer4.EntityFramework.Entities.ApiScope>()
                {
                    new IdentityServer4.EntityFramework.Entities.ApiScope()
                    {
                        Name  = "API",
                        UserClaims = new List<IdentityServer4.EntityFramework.Entities.ApiScopeClaim>() { new IdentityServer4.EntityFramework.Entities.ApiScopeClaim()
                        {

                        } }
                    }
                }
            });

            _dbContext.IdentityResources.Add(new IdentityServer4.EntityFramework.Entities.IdentityResource()
            {
                Name = "API",
                UserClaims = new List<IdentityServer4.EntityFramework.Entities.IdentityClaim>()
                {
                    new IdentityServer4.EntityFramework.Entities.IdentityClaim() { Type = ""  }
                }
            });
            return null;
        }
    }
}
