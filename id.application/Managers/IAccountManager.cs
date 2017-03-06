using id.application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.application.Managers
{
    public interface IAccountManager
    {
        XClient XClientAdded { get; }
        XClient InitClient();
        XClient GetClient(int clientId);
        List<XClient> GetListClient();
    }
}
