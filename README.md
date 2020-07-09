# Material API
Basic training project to provide a RESTful API using ASP.NET Core 3.1 and RavenDB

> **TIP**: The API includes Swagger as documentation and basic test possibility. It provides basic descriptions and example JSON objects for all provided endpoints. It is also possible to easily call the endpoints for manually testing there. To open Swagger UI open route `\swagger`

## Goals
### Done
* Implement API using .NET Core 3.1
* Provide following routes
  * GET /material/:id 
  * GET /material?name={name} 
  * POST /material 
  * PUT /material 
  * DELETE /material/:id

### Pending
* Persist Data using RavenDB
* Testing using xUnit 

### Out of Scope
The here mentioned things are currently out of scope, but would increase the quality of the API
* Additional repository layer between service and database access
* Proper filtering for all properties instead of simple name only filter


