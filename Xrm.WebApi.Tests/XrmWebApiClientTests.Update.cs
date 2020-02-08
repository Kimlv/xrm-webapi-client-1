/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading.Tasks;

using Xrm.WebApi.Tests.Entities;

namespace Xrm.WebApi.Tests
{
    public partial class XrmWebApiClientTests
    {
        [TestMethod]
        [TestCategory("Update")]
        [TestCategory("Positive")]
        public async Task UpdateAsync_WithData_ShouldReturnSuccess()
        {
            var record = new Contact
            {
                FirstName = "XrmWebApiClient_Update"
            };

            await _xrmWebApiClient.UpdateAsync<Contact>(_recordId, record);
        }
    }
}