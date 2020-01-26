# xrm-webapi-client

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/db57100548854f228826324d204b4ea5)](https://www.codacy.com/manual/off-world/xrm-webapi-client?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=wunderjunge/xrm-webapi-client&amp;utm_campaign=Badge_Grade)

A type-safe and generic Dynamics 365 Xrm Web Api Client for .NET Core

## Example Usage

Define data classes representing the Xrm entity and properties of interest. Implement the `IXrmWebApiQueryable` interface and decorate the properties according to the schema names as returned by the Xrm Web Api if necessary.

```CSharp
using System.Text.Json;

using Xrm.WebApi;

class Account : IXrmWebApiQueryable
{
    public string EntityLogicalNamePlural => "accounts";

    [JsonPropertyName("accountid")
    public string? Id { get; set; }
    
    [JsonPropertyName("statecode")
    public int? StateCode { get; set; }
    
    ...
}
```

Initialize and connect the `XrmWebApiClient` to your organization's Dynamics 365 Crm instance by providing the required connection and authentication information.

```CSharp
// your organization's azure tenant id
var tenant = "6d1708ce-bb10-4579-a5e6-25268764c36a";
// your organization's Dynamics Crm Online service root uri
var serviceRootUri = new Uri("https://your-organization.crm4.dynamics.com/api/data/v9.1/");
// your applications client credentials as registered in your organization's Azure AD
var credentials = new ClientCredentials("e8b89848-be54-4a01-b953-022a164016ce", "...");

// Initialize and connect
var xrmClient = await XrmWebApiClient.ConnectAsync(serviceRootUri, credentials, tenant);
```
