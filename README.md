# Material API
Basic training project to get some experience in using ASP.NET Core 3.1, RavenDB and xUnit. 

The project provides a RESTful API to store, change and request materials.

## Goals
### Done
* Implement API using .NET Core 3.1
* Provide following routes:
  * GET /materials/:id 
  * GET /materials?name={name} 
  * POST /materials 
  * PUT /materials
  * DELETE /materials/:id
* Provide Swagger 
* Persist in RavendDB 
  * Unsecured Database
  * Secured Database (using certificate only)
* Exemplary Unit Tests using xUnit
* Basic Exception Handling
* Basic Logging 
* Async processing

### Out of Scope
The here mentioned things are currently out of scope because my main focus is to get some experience in using ASP.NET Core and RavenDB. But of cf course they would increase the quality of the API and I might tackle them later
* Authentication
* Additional repository layer between service and database access 
* Filtering for all properties instead of the currently provided simple name only filter
* Additional Unit and Integration tests
* Additional Exception handling and logging

## Run it
### Swagger
The API includes Swagger as documentation and basic test possibility. It provides basic descriptions and example JSON objects for all provided endpoints. It is also possibility to easily call the endpoints for manually testing there. To open Swagger UI open route `\swagger` or just use the respective launch profile which will open it immediately

### RavenDB
This project expects a RavenDB database to persist data. In the appsettings.json (or appsettings.Development.json) you can provide the needed configuration, for unsecured databases just provide an url and a database. 

```
    "DocumentStore": {
        "Url": "http://yourRavenDB:Port",
        "Database": "DatabaseName"
    }
```
For secured access you additionally must provide a path to the client certificate provided by RavenDB. 
```
    "DocumentStore": {
        "Url": "https://yourRavenDB:Port",
        "Database": "DatabaseName",
        "PathToCertificate": "../yourClientCertificate.pfx"
    }
```

### In Memory
If you want to run this without a ravenDB, you can switch the comment for the following lines in the Startup.cs 
```
//to run the API without RavendDB use the commented line instead
services.AddSingleton<IMaterialService, MaterialService>();
//services.AddSingleton<IMaterialService, MaterialServiceFake>();`
```


