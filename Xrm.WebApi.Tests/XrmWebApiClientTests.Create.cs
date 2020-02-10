/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading.Tasks;

using Xrm.WebApi.Exceptions;
using Xrm.WebApi.Tests.Entities;

namespace Xrm.WebApi.Tests
{
    public partial class XrmWebApiClientTests
    {
        [TestMethod]
        [TestCategory("Create")]
        [TestCategory("Positive")]
        public async Task CreateAsync_WithData_ShouldReturnResult_ThenDeleteAsync()
        {
            var record = new Contact
            {
                LastName = "XrmWebApiClientTestUser"
            };

            var id = await _xrmWebApiClient.CreateAsync<Contact>(record);

            Assert.IsNotNull(id);
            Assert.IsInstanceOfType(id, typeof(Guid));

            await _xrmWebApiClient.DeleteAsync<Contact>(id);
        }

        [TestMethod]
        [TestCategory("Create")]
        [TestCategory("Negative")]
        public async Task CreateAsync_WithInvalidData_ShouldThrowXrmWebApiException()
        {
            await Assert.ThrowsExceptionAsync<XrmWebApiException>(async () =>
            {
                var record = new Contact
                {
                    PropertyShouldNotExist = "invalid_property"
                };

                await _xrmWebApiClient.CreateAsync<Contact>(record);
            });
        }

        [TestMethod]
        [TestCategory("Create")]
        [TestCategory("Negative")]
        public async Task CreateAsync_WhenEntityClassIsMissingAttributes_ShouldThrowMissingAttributeException()
        {
            await Assert.ThrowsExceptionAsync<MissingAttributeException>(async () =>
            {
                var record = new Account();

                await _xrmWebApiClient.CreateAsync<Account>(record);
            });
        }
    }
}