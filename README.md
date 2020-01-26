# xrm-webapi-client

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/db57100548854f228826324d204b4ea5)](https://www.codacy.com/manual/off-world/xrm-webapi-client?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=wunderjunge/xrm-webapi-client&amp;utm_campaign=Badge_Grade)

A type-safe and generic Dynamics 365 Xrm Web Api Client for .NET Core

## Example Usage

1.  Define data classes representing the Xrm entities and properties of interest. Implement the `IXrmWebApiQueryable` interface and decorate the properties according to the schema names as returned by the Xrm Web Api if necessary.

```CSharp
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

2.  Initialize and connect the `XrmWebApiClient` to your organization's Dynamics 365 Crm instance by providing the required connection and authentication information.

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

3.  Use OData system queries to communicate with the Xrm Web Api

```CSharp
// Get all active accounts
List<Account> accounts = await client
    .RetrieveMultipleAsync<Account>("?$select=accountid&$filter=statecode eq 0");
```

## Tests

To run the Unit Tests provide the following organisation-specific connection and authentication information and test data in a `secrets.json` managed by the [Secrets Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows#secret-manager).  

```Json
{
    "TestConnection": {
        "Tenant": "<tenant id>",
        "ResourceUri": "<service root uri>",
        "Credentials": {
            "ClientId": "<client id>",
            "ClientSecret": "<client secret>"
        }
    },
    "TestData": {
        "ContactId": "<Id of an existing contact record>"
    }
}
```

## ToDo

*  [ ] Create Record
*  [ ] Update Record
*  [ ] Delete Record