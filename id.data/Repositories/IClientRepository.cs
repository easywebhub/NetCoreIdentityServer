using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.data.Repositories
{
    public interface IClientRepository: IGenericRepository<Client>
    {
    }
}
