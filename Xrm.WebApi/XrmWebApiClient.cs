/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xrm.WebApi.Responses;
using Xrm.WebApi.Exceptions;

namespace Xrm.WebApi
{
    /// <summary>
    /// <see cref="HttpClient"/> wrapper to communicate with the Dynamics 365 Xrm Web Api.
    /// </summary>
    public sealed class XrmWebApiClient
    {
        private readonly HttpClient _httpClient;

        private XrmWebApiClient(HttpClient httpClient) => _httpClient = httpClient;

        /// <summary>
        /// Factory method to create and initialize a generic Dynamics 365 <see cref="XrmWebApiClient">
        /// using an OAuth2 Credential Grant Flow establishing a service-to-service connection.
        /// </summary>
        /// <returns>
        /// A ready-to-use <see cref="XrmWebApiClient"> set-up with a valid access token to
        /// query the Dynamics 365 Xrm Web Api.
        /// </returns>
        /// <param name="serviceRootUri">
        /// Service Root Uri of the Dynamics 365 Crm instance to connect.
        /// </param>
        /// <param name="credentials">
        /// Authentication information in the form of <see cref="ClientCredentials"/>.
        /// </param>
        /// <param name="tenant">
        /// Tenant Id or hostname the Dynamics 365 Crm instance lives in (Optional).
        /// Uses the common endpoint by default.
        /// </param>
        /// <remarks>
        /// see also <a href="https://docs.microsoft.com/en-us/azure/active-directory/develop/v1-oauth2-client-creds-grant-flow"/>
        /// </remarks>
        public static async Task<XrmWebApiClient> ConnectAsync(Uri serviceRootUri, ClientCredentials credentials, string tenant = "common")
        {
            using var client = new HttpClient();

            // build an access token request with a shared secret
            var formContent = new FormUrlEncodedContent(new[]
            {
                // the app id uri of the receiving web service which is the dynamics 365 crm instance service root uri
                new KeyValuePair<string, string>("resource", serviceRootUri.GetLeftPart(UriPartial.Authority)),
                // the azure AD client id of the calling web service
                new KeyValuePair<string, string>("client_id", credentials.ClientId),
                // a key registered for the calling web service or daemon application in Azure AD
                new KeyValuePair<string, string>("client_secret", credentials.Secret),
                // grant type in a Client Credentials Grant Flow must be 'client_credentials'
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            // request an access token
            HttpResponseMessage response =
                await client.PostAsync($"https://login.microsoftonline.com/{tenant}/oauth2/token", formContent);

            // throw error if access token http request failed
            response.EnsureSuccessStatusCode();

            // parse access token response as json
            var content = await response.Content.ReadAsStringAsync();
            // parse access token response from json
            var accessTokenResponse = JsonSerializer.Deserialize<AccessTokenResponse>(content);

            // create wep api http client
            var httpClient = new HttpClient
            {
                BaseAddress = serviceRootUri
            };

            // initialize web api http client
            httpClient.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            httpClient.DefaultRequestHeaders.Add("OData-Version", "4.0");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessTokenResponse.AccessToken);

            return new XrmWebApiClient(httpClient);
        }

        /// <summary>
        /// Retrieves a single entity record.
        /// </summary>
        /// <typeparam name="T">The entity to query</typeparam>
        /// <param name="id">Guid of the record to retrieve</param>
        /// <param name="options">OData system query options as supported by the Xrm Web Api</param>
        /// <returns>A record of <typeparamref name="T"/>.</returns>
        /// <remarks>
        /// see <a href="https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/reference/xrm-webapi/retrieverecord"/>
        /// </remarks>
        public async Task<T> RetrieveAsync<T>(string id, string options = "")
        {
            // ensure T is decorated with the required attribute in order to retrieve records
            var attribute = TryResolveAttribute<EntityLogicalCollectionNameAttribute>(typeof(T));

            // query the web api
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{attribute.EntityLogicalCollectionName}({id}){options}");

            // parse web api response as stream
            var content = await response.Content.ReadAsStreamAsync();

            try
            {
                // try parsing a record from json
                var record = await JsonSerializer.DeserializeAsync<T>(content);

                // throw if the http request failed or the web api returned an error
                response.EnsureSuccessStatusCode();

                return record;
            }
            catch
            {
                throw new XrmWebApiException(response);
            }
        }

        /// <summary>
        /// Retrieves a collection of entity records.
        /// </summary>
        /// <typeparam name="T">The entity to query</typeparam>
        /// <param name="options">OData system query options as supported by the Xrm Web Api</param>
        /// <returns>A list of records of <typeparamref name="T"/>.</returns>
        /// <remarks>
        /// see <a href="https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/reference/xrm-webapi/retrievemultiplerecords"/>
        /// </remarks>
        public async Task<List<T>> RetrieveMultipleAsync<T>(string options)
        {
            // ensure T is decorated with the required attribute in order to retrieve records
            var attribute = TryResolveAttribute<EntityLogicalCollectionNameAttribute>(typeof(T));

            // query the web api
            HttpResponseMessage response =
                await _httpClient.GetAsync($"{attribute.EntityLogicalCollectionName}{options}");

            // parse web api response as stream
            var content = await response.Content.ReadAsStreamAsync();

            try
            {
                // try parsing a collection of records from json
                var multipleRecordsResponse =
                    await JsonSerializer.DeserializeAsync<MultipleRecordsResponse<T>>(content);

                // throw if the http request failed or the web api returned an error
                response.EnsureSuccessStatusCode();

                // cannot be null here as the web api either returns results or
                // an error. The latter is handled above by ensuring an http
                // success status code and any json errors are catched, too.
                return multipleRecordsResponse.Results!;
            }
            catch
            {
                throw new XrmWebApiException(response);
            }
        }

        private static A TryResolveAttribute<A>(Type type)
            where A : Attribute
        {
            A? attribute = (A?)Attribute.GetCustomAttribute(type, typeof(A));

            if (attribute == null)
            {
                throw new MissingAttributeException(nameof(type), nameof(A));
            }

            return attribute;
        }
    }
}
