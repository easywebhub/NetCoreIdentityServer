using id.common.Entities;
using id.data.Repositories;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace id.application.Entities
{
    public class XClient : XEntityBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IXMapper _xMapper;

        public XClient(IClientRepository clientRepository, IXMapper xMapper)
        {
            _clientRepository = clientRepository;
            _xMapper = xMapper;
        }
        public XClient(Client client, IClientRepository clientRepository, IXMapper xMapper) : this(clientRepository, xMapper)
        {
            this._client = client;
            MapFrom(client);
        }

        #region properties
        public int ClientId { get; set; }
        public string ClientIdName { get; set; }
        public string ClientName { get; set; }
        public bool Enabled { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public string LogoutUri { get; set; }
        public List<ClientSecret> ClientSecrets { get; set; }
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
        public List<ClientScope> AllowedScopes { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public List<ClientGrantType> AllowedGrantTypes { get; set; }


        #endregion

        #region ext properties
        private Client _client;
        #endregion

        #region methods
        public bool IsExits()
        {
            return ClientId > 0;
        }
        public bool Save()
        {
            if (IsExits())
            {
                _clientRepository.Edit(_xMapper.ToEntity(_client, this));
            }
            else
            {
                _clientRepository.Add(_xMapper.ToEntity(new Client(), this));
            }
            _clientRepository.Save();
            return true;
        }
        #endregion

        #region private methods
        private void MapFrom(Client client)
        {
            this.ClientId = client.Id;
            this.ClientIdName = client.ClientId;
            this.ClientName = client.ClientName;
            this.Enabled = client.Enabled;
            this.AllowOfflineAccess = client.AllowOfflineAccess;
            this.ClientSecrets = client.ClientSecrets ?? new List<ClientSecret>();
            this.Claims = client.Claims ?? new List<ClientClaim>();
            this.AllowedScopes = client.AllowedScopes ?? new List<ClientScope>();
            this.AllowedGrantTypes = client.AllowedGrantTypes ?? new List<ClientGrantType>();
        }
        #endregion
    }
}
