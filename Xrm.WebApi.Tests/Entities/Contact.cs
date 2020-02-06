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
    /// Json model for the Dynamics 365 Xrm contact entity.
    /// </summary>
    [EntityLogicalCollectionName("contacts")]
    internal class Contact
    {
        /// <summary>
        /// The guid of the contact.
        /// </summary>
        [JsonPropertyName("contactid")]
        public string? ContactId { get; set; }
    }
}
