/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

namespace Xrm.WebApi
{
    /// <summary>
    /// Client credential storage.
    /// </summary>
    public sealed class ClientCredentials
    {
        /// <summary>
        /// The applications client id as registered in Azure AD.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The applications client secret as registered in Azure AD.
        /// </summary>
        public string Secret { internal get; set; }

        /// <summary>
        /// Initializes a <see cref="ClientCredentials"/> instance.
        /// </summary>
        /// <param name="clientId">
        /// The applications client id as registered in Azure AD.
        /// </param>
        /// <param name="secret">
        /// The applications client secret as registered in Azure AD.
        /// </param>
        public ClientCredentials(string clientId, string secret)
        {
            ClientId = clientId;
            Secret = secret;
        }
    }
}
