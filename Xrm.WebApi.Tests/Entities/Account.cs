/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System.Text.Json.Serialization;

namespace Xrm.WebApi.Tests.Entities
{
    /// <summary>
    /// Json model for the Dynamics 365 Xrm account entity.
    /// Missing <see cref="EntityLogicalCollectionNameAttribute"/>
    /// and <see cref="EntityLogicalNameAttribute"/>.
    /// </summary>
    internal class Account
    {
        /// <summary>
        /// The guid of the account.
        /// </summary>
        [JsonPropertyName("Accountid")]
        public string? AccountId { get; set; }
    }
}
