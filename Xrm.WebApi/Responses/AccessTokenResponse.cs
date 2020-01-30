/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System.Text.Json.Serialization;

namespace Xrm.WebApi.Responses
{
    /// <summary>
    /// Json model for an OAuth2 access token response.
    /// </summary>
    internal sealed class AccessTokenResponse
    {
        /// <summary>
        /// The type of token, typically "Bearer".
        /// </summary>
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        /// <summary>
        /// The access token as issued by the authorization server.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
    }
}
