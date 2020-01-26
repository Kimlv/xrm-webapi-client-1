/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;
using System.Net.Http;
using System.Text.Json;

using Xrm.WebApi.Response;

namespace Xrm.WebApi
{
    /// <summary>
    /// Exception thrown on Dynamics 365 Xrm Web Api request failure.
    /// </summary>
    public class XrmWebApiException : Exception
    {
        /// <summary>
        /// Initializes a new <see cref="XrmWebApiException"/>.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage"/> from the Xrm Web Api request</param>
        public XrmWebApiException(HttpResponseMessage response) :
            base (ParseError(response))
        {
        }

        private static string ParseError(HttpResponseMessage response)
        {
            // parse web api response as string
            var content = response.Content.ReadAsStringAsync().Result;

            try
            {
                // try parsing a web api error from json
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content);

                if (errorResponse.Error != null)
                {
                    return errorResponse.Error.Message!;
                }
            }
            catch
            {
                // return the original http error message
                if (!response.IsSuccessStatusCode)
                {
                    return response.ReasonPhrase;
                }
            }

            return "Unexpected Error";
        }
    }
}
