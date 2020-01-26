/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

using System;

namespace Xrm.WebApi.Tests
{
    [TestClass]
    public partial class XrmWebApiClientTests
    {
        private XrmWebApiClient _xrmWebApiClient;

        private string _resourceUri;
        private string _tenant;
        private string _clientId;
        private string _secret;

        private string _contactId;

        /*
         * disable warning CS8618 here as fields are ensured to be initialized by the Initialize-methods
         */

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public XrmWebApiClientTests()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<XrmWebApiClientTests>()
                .Build();

            InitializeTestData(configuration);
            InitializeTestConnection(configuration);
        }

        private void InitializeTestData(IConfigurationRoot configuration)
        {
            var testData = configuration.GetSection("TestData");

            _contactId = testData["ContactId"]          ?? throw new ArgumentNullException("ContactId");
        }

        private void InitializeTestConnection(IConfigurationRoot configuration)
        {
            var connection = configuration.GetSection("TestConnection");

            _tenant = connection["Tenant"]              ?? throw new ArgumentNullException("Tenant");
            _resourceUri = connection["ResourceUri"]    ?? throw new ArgumentNullException("ResourceUri");

            var credential = connection.GetSection("Credentials");

            _clientId = credential["ClientId"]          ?? throw new ArgumentNullException("ClientId");
            _secret = credential["ClientSecret"]        ?? throw new ArgumentNullException("ClientSecret");

            _xrmWebApiClient = XrmWebApiClient
                .ConnectAsync(new Uri(_resourceUri!), new ClientCredentials(_clientId, _secret), _tenant).Result;
        }
    }
}
