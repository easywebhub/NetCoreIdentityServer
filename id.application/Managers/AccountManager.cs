using id.common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using id.application.Entities;
using id.data.Repositories;
using id.common;

namespace id.application.Managers
{
    public class AccountManager : XEntityBase, IAccountManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly IXMapper _xMapper;

        public AccountManager(IClientRepository clientRepository, IXMapper xMapper)
        {
            _clientRepository = clientRepository;
            _xMapper = xMapper;
        }

        public XClient XClientAdded { get; private set; }

        public XClient GetClient(int clientId)
        {
            var client = _clientRepository.Get(clientId);
            if (client == null)
            {
                XStatus = GlobalStatus.NotFound;
                return null;
            }
            return new XClient(client, _clientRepository, _xMapper);
        }

        public List<XClient> GetListClient()
        {
            return _xMapper.ToXClients(_clientRepository.GetAll().ToList());
        }

        public XClient InitClient()
        {
            throw new NotImplementedException();
        }
    }
}
