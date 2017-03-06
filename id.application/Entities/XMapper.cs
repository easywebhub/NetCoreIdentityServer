using id.data.Repositories;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.application.Entities
{
    public interface IXMapper
    {
        Client ToEntity(Client client, XClient xClient);
        List<XClient> ToXClients(List<Client> clients);
    }
    public class XMapper : IXMapper
    {
        private readonly IClientRepository _clientRepository;

        public Client ToEntity(Client client, XClient xClient)
        {
            client.ClientId = xClient.ClientIdName;
            client.ClientName = xClient.ClientName;
            client.AllowOfflineAccess = xClient.AllowOfflineAccess;
            client.Enabled = xClient.Enabled;
            client.LogoutUri = xClient.LogoutUri;
            client.PostLogoutRedirectUris = xClient.PostLogoutRedirectUris;
            client.ClientSecrets = xClient.ClientSecrets;
            client.Claims = xClient.Claims;
            client.AllowedScopes = xClient.AllowedScopes;
            client.AllowedGrantTypes = xClient.AllowedGrantTypes;

            return client;
        }

        public List<XClient> ToXClients(List<Client> clients)
        {
            return clients.Select(x => new XClient(x, _clientRepository, this)).ToList();
        }
    }
}
