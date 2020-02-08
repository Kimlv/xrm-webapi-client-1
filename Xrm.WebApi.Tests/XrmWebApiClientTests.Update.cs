/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;

using Xrm.WebApi.Exceptions;
using Xrm.WebApi.Tests.Entities;

namespace Xrm.WebApi.Tests
{
    public partial class XrmWebApiClientTests
    {
        [TestMethod]
        [TestCategory("Update")]
        [TestCategory("Positive")]
        public async Task UpdateAsync_WithData_ShouldReturnWithSuccess()
        {
            var record = new Contact
            {
                FirstName = "XrmWebApiClient_Update"
            };

            await _xrmWebApiClient.UpdateAsync<Contact>(_recordId, record);
        }

        [TestMethod]
        [TestCategory("Update")]
        [TestCategory("Negative")]
        public async Task UpdateAsync_WithInvalidData_ShouldThrowXrmWebApiException()
        {
            await Assert.ThrowsExceptionAsync<XrmWebApiException>(async () =>
            {
                var record = new Contact
                {
                    PropertyShouldNotExist = "invalid_property"
                };

                await _xrmWebApiClient.UpdateAsync<Contact>(_recordId, record);
            });
        }

        [TestMethod]
        [TestCategory("Update")]
        [TestCategory("Negative")]
        public async Task UpdateAsync_WhenEntityClassIsMissingAttributes_ShouldThrowMissingAttributeException()
        {
            await Assert.ThrowsExceptionAsync<MissingAttributeException>(async () =>
            {
                var record = new Account();

                await _xrmWebApiClient.UpdateAsync<Account>(_recordId, record);
            });
        }
    }
}