﻿/*
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
        [TestCategory("Retrieve")]
        public async Task RetrieveMultipleAsync_Top3_ShouldReturn3Results()
        {
            var options = "?$top=3";
            var contacts = await _xrmWebApiClient.RetrieveMultipleAsync<Contact>(options);

            Assert.IsTrue(contacts.Count == 3);
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveMultipleAsync_SelectIdFromTop3_ShouldReturn3ResultsWithId()
        {
            var options = "?$select=contactid&$top=3";
            var contacts = await _xrmWebApiClient.RetrieveMultipleAsync<Contact>(options);

            foreach (var contact in contacts)
            {
                Assert.IsNotNull(contact.ContactId);
            }
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveMultipleAsync_WhenIdEqualsNonExistentId_ShouldReturnNoResults()
        {
            var options = "?$filter=contactid eq 00000000-0000-0000-0000-000000000000";
            var contacts = await _xrmWebApiClient.RetrieveMultipleAsync<Contact>(options);

            Assert.IsTrue(contacts.Count == 0);
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveMultipleAsync_WithInvalidOptions_ShouldThrowXrmWebApiException()
        {
            await Assert.ThrowsExceptionAsync<XrmWebApiException>(async () =>
            {
                var options = "?$invalid_option";
                await _xrmWebApiClient.RetrieveMultipleAsync<Contact>(options);
            });
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveMultipleAsync_WhenEntityClassIsMissingAttributes_ShouldThrowMissingAttributeException()
        {
            await Assert.ThrowsExceptionAsync<MissingAttributeException>(async () =>
            {
                var options = "";
                await _xrmWebApiClient.RetrieveMultipleAsync<Account>(options);
            });
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveAsync_WithValidGuid_ShouldReturnResult()
        {
            var id = new Guid(_recordId);

            var contact = await _xrmWebApiClient.RetrieveAsync<Contact>(id);

            Assert.IsNotNull(contact);
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveAsync_WithValidId_ShouldReturnResult()
        {
            var contact = await _xrmWebApiClient.RetrieveAsync<Contact>(_recordId);

            Assert.IsNotNull(contact);
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveAsync_SelectIdFromRecord_ShouldReturnResultWithId()
        {
            var options = "?$select=contactid";
            var contact = await _xrmWebApiClient.RetrieveAsync<Contact>(_recordId, options);

            Assert.IsNotNull(contact);
            Assert.IsNotNull(contact!.ContactId);
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveAsync_WhenIdEqualsNonExistentId_ShouldThrowXrmWebApiException()
        {
            await Assert.ThrowsExceptionAsync<XrmWebApiException>(async () =>
            {
                var id = "00000000-0000-0000-0000-000000000000";
                await _xrmWebApiClient.RetrieveAsync<Contact>(id);
            });
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveAsync_WithInvalidOptions_ShouldThrowXrmWebApiException()
        {
            await Assert.ThrowsExceptionAsync<XrmWebApiException>(async () =>
            {
                var options = "?$invalid_option";
                await _xrmWebApiClient.RetrieveAsync<Contact>(_recordId, options);
            });
        }

        [TestMethod]
        [TestCategory("Retrieve")]
        public async Task RetrieveAsync_WhenEntityClassIsMissingAttributes_ShouldThrowMissingAttributeException()
        {
            await Assert.ThrowsExceptionAsync<MissingAttributeException>(async () =>
            {
                var options = "";
                await _xrmWebApiClient.RetrieveAsync<Account>(_recordId, options);
            });
        }
    }
}
