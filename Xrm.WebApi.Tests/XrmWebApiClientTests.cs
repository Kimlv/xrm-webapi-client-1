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

        private string _serviceRoot;
        private string _tenant;
        private string _clientId;
        private string _secret;

        private string _recordId;

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

            _recordId = testData["RecordId"] ?? throw new ArgumentNullException("RecordId");
        }

        private void InitializeTestConnection(IConfigurationRoot configuration)
        {
            var connection = configuration.GetSection("TestConnection");

            _tenant = connection["Tenant"] ?? throw new ArgumentNullException("Tenant");
            _serviceRoot = connection["ServiceRoot"] ?? throw new ArgumentNullException("Resource");

            var credential = connection.GetSection("Credentials");

            _clientId = credential["ClientId"] ?? throw new ArgumentNullException("ClientId");
            _secret = credential["Secret"] ?? throw new ArgumentNullException("Secret");

            _xrmWebApiClient = XrmWebApiClient
                .ConnectAsync(new Uri(_serviceRoot), new ClientCredentials(_clientId, _secret), _tenant).Result;
        }
    }
}
