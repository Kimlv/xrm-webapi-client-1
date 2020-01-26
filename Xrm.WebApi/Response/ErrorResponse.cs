/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System.Text.Json.Serialization;

namespace Xrm.WebApi.Response
{
    /// <summary>
    /// Json model for a Dynamics 365 Xrm Web Api response containing an error.
    /// </summary>
    internal sealed class ErrorResponse
    {
        /// <summary>
        /// The error containing error information on failure.
        /// </summary>
        [JsonPropertyName("error")]
        public Error? Error { get; set; }
    }

    /// <summary>
    /// Json model for a Dynamics 365 Web Api error.
    /// </summary>
    internal sealed class Error
    {
        /// <summary>
        /// The error code returned by the web api response.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        /// The error message returned by the web api response.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
