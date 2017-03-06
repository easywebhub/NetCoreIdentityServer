using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace id.data.Repositories
{
    public interface IIdentityResourceRepository : IGenericRepository<IdentityResource>
    {
    }
}
