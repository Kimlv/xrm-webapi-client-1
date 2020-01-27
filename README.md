# xrm-webapi-client

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/db57100548854f228826324d204b4ea5)](https://www.codacy.com/manual/off-world/xrm-webapi-client?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=wunderjunge/xrm-webapi-client&amp;utm_campaign=Badge_Grade)

A type-safe and generic Dynamics 365 Xrm Web Api Client for .NET Core

## Getting Started

### Define your Business Data

Define data classes representing the Xrm entities and properties of interest. Implement the `IXrmWebApiQueryable` interface and decorate the properties according to the schema names as returned by the Xrm Web Api if necessary.

```CSharp
class Account : IXrmWebApiQueryable
{
    public string EntityLogicalNamePlural => "accounts";

    [JsonPropertyName("accountid")]
    public string? Id { get; set; }
    
    [JsonPropertyName("statecode")]
    public int? StateCode { get; set; }
    
    ...
}
```

### Connect to your Organization

Initialize and connect the `XrmWebApiClient` to a Dynamics 365 Crm instance by providing the required connection and authentication information.

```CSharp
// Azure Tenant id
var tenantId = "6d1708ce-bb10-4579-a5e6-25268764c36a";

// Dynamics Crm Online service root uri
var serviceRootUri = new Uri("https://contoso.crm4.dynamics.com/api/data/v9.1/");

// Azure AD registered application's client id and secret
var credentials = new ClientCredentials("e8b89848-be54-4a01-b953-022a164016ce", "...");
```
```CSharp
// Initialize and connect
XrmWebApiClient client = await XrmWebApiClient
    .ConnectAsync(serviceRootUri, credentials, tenant);
```

### Communicate with Dynamics

Use OData system queries to communicate with the Xrm Web Api

```CSharp
// Get all active accounts
List<Account> accounts = await client
    .RetrieveMultipleAsync<Account>("?$select=accountid&$filter=statecode eq 0");
```

## Build and Test

1. Download or clone the repo and open the `Xrm.WebApi.sln` Solution in Visual Studio.
2. Build the `Xrm.WebApi.csproj` and `Xrm.Webapi.Tests.csproj` projects
3. To run the Unit Tests provide the following organization-specific information and test data in a `secrets.json` file managed by the [Secrets Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#secret-manager).  

```Json
{
    "TestConnection": {
        "Tenant": "<tenant id>",
        "ServiceRoot": "<service root uri>",
        "Credentials": {
            "ClientId": "<client id>",
            "Secret": "<client secret>"
        }
    },
    "TestData": {
        "RecordId": "<Id of an existing contact record>"
    }
}
```

## ToDo

* [ ] Add method to Create Record
* [ ] Add method to Update Record
* [ ] Add method to Delete Record
* [ ] Publish to NuGet