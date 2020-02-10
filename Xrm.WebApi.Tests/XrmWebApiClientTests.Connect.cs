/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Xrm.WebApi.Tests
{
    public partial class XrmWebApiClientTests
    {
        [TestMethod]
        [TestCategory("Connect")]
        public void ConnectAsync_WithValidConfiguration_ShouldReturnXrmWebApiClient()
        {
            Assert.IsNotNull(_xrmWebApiClient);
            Assert.IsInstanceOfType(_xrmWebApiClient, typeof(XrmWebApiClient));
        }

        [TestMethod]
        [TestCategory("Connect")]
        public async Task ConnectAsync_WhenClientIdIsInvalid_ShouldThrowHttpRequestException()
        {
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                var clientId = "INVALID_CLIENT_ID";

                await XrmWebApiClient.ConnectAsync(new Uri(_serviceRoot), new ClientCredentials(clientId, _secret), _tenant);
            });
        }

        [TestMethod]
        [TestCategory("Connect")]
        public async Task ConnectAsync_WhenClientSecretIsInvalid_ShouldThrowHttpRequestException()
        {
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                var secret = "INVALID_CLIENT_SECRET";

                await XrmWebApiClient.ConnectAsync(new Uri(_serviceRoot), new ClientCredentials(_clientId, secret), _tenant);
            });
        }

        [TestMethod]
        [TestCategory("Connect")]
        public async Task ConnectAsync_WhenResourceUriIsInvalid_ShouldThrowHttpRequestException()
        {
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                var resourceUri = "https://invalid.resource.uri";

                await XrmWebApiClient.ConnectAsync(new Uri(resourceUri), new ClientCredentials(_clientId, _secret), _tenant);
            });
        }

        [TestMethod]
        [TestCategory("Connect")]
        public async Task ConnectAsync_WhenTenantIsInvalid_ShouldThrowHttpRequestException()
        {
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                var tenant = "00000000-0000-0000-0000-000000000000";

                await XrmWebApiClient.ConnectAsync(new Uri(_serviceRoot), new ClientCredentials(_clientId, _secret), tenant);
            });
        }
    }
}
