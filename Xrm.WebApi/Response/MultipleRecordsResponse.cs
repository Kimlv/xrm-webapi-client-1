/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Xrm.WebApi.Response
{
    /// <summary>
    /// Json model for a Dynamics 365 Xrm Web Api response containing a collection of records.
    /// </summary>
    internal sealed class MultipleRecordsResponse<T>
        where T : IXrmWebApiQueryable
    {
        /// <summary>
        /// The value containing the odata query results when multiple records are retrieved.
        /// </summary>
        [JsonPropertyName("value")]
        public List<T>? Results { get; set; }
    }
}
