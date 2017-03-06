﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0
// Author:					Joe Audette
// Created:					2016-10-13
// Last Modified:			2016-10-17
// 

using IdentityServer4.Models;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Storage
{
    public interface IClientCommands
    {
        Task CreateClient(string siteId, Client client, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateClient(string siteId, Client client, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteClient(string siteId, string clientId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
