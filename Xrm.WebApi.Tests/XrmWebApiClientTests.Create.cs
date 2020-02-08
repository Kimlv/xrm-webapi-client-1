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
        [TestCategory("Create")]
        [TestCategory("Positive")]
        public async Task CreateAsync_WithData_ShouldReturnResult()
        {
            var record = new Contact()
            {
                FirstName = "Alfred",
                LastName = "Quack"
            };

            var id = await _xrmWebApiClient.CreateAsync<Contact>(record);

            Assert.IsNotNull(id);
            Assert.IsInstanceOfType(id, typeof(Guid));
        }

        [TestMethod]
        [TestCategory("Create")]
        [TestCategory("Positive")]
        public async Task CreateAsync_WithNoData_ShouldReturnResult()
        {
            var record = new Contact();

            var id = await _xrmWebApiClient.CreateAsync<Contact>(record);

            Assert.IsNotNull(id);
            Assert.IsInstanceOfType(id, typeof(Guid));
        }
    }
}