# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

A REST API for people and books built on ASP.NET and .NET 10 (LTS), SQL Server, EF Core and Evolve, root namespace `RestWithASPNET10Erudio`. Besides the CRUD it already has HATEOAS links, pagination, JSON and XML content negotiation, CSV and XLSX import/export, file upload/download, e-mail sending, CORS, and Swagger plus Scalar documentation. It is the working project for a "skills in action" video series where **JWT authentication** is added with Claude Code. The target state:

- `Microsoft.AspNetCore.Authentication.JwtBearer` with the setup in `Configurations/AuthConfig.cs`, and a `TokenConfigurations` section in `appsettings.json`
- A `User` entity, an `IUserRepository`, and a `Users` table created by a new Evolve migration, with a seed user in the dataset folder
- An `Auth` folder with the token configuration, `ITokenGenerator`/`TokenGenerator` and `IPasswordHasher`/`Sha256PasswordHasher`
- `ILoginService`/`LoginServiceImpl` and `IUserAuthService`/`UserAuthServiceImpl`
- An `AuthController` at `api/auth` with `signin`, `refresh`, `revoke` (authenticated) and `create`
- `[Authorize("Bearer")]` on the Person, Book, Email and File controllers
- `app.UseAuthentication()` before `app.UseAuthorization()` in `Program.cs`

None of that exists yet: every endpoint is open. The existing endpoints and their contracts must not change, except that the protected ones start requiring a Bearer token.

## Commands

```bash
dotnet build RestWithASPNET10Erudio.sln
dotnet run --project RestWithASPNET10Erudio                          # needs SQL Server (see Runtime setup)
dotnet test                                                          # everything: needs Docker AND a SQL Server
dotnet test --filter-namespace "*UnitTests*"                         # unit tests only, no Docker
dotnet test --filter-class "*PersonControllerJsonTests"              # one class (run whole classes, not single tests)
```

All commands run from this folder (`02-authentication-after`).

## Tests

- `UnitTests` are plain xUnit tests with no external dependency.
- `IntegrationTests` use `WebApplicationFactory`, a SQL Server container started by Testcontainers (`SqlServerFixture`, the first run downloads the image, about 2 GB) and an ordered suite: each class runs its methods in `[TestPriority(n)]` order and later tests depend on data created by earlier ones. Run whole classes, never a single method.
- **The integration tests also need a SQL Server on `localhost,1433`.** `WebApplicationFactory` runs the app in `Development`, and `AddEvolveConfiguration` migrates the database from the connection string in `appsettings.json` before the test configuration is applied. Without that server every integration test fails with an Evolve connection timeout.
- xUnit v3 runs through Microsoft.Testing.Platform on the .NET 10 SDK: the `global.json` next to the solution turns that on. Without it `dotnet test` fails with "Testing with VSTest target is no longer supported". Filters use `--filter-class` / `--filter-namespace`, not `--filter`.
- The ordering uses an `ITestMethodOrderer` (`PriorityOrderer`) applied with `[TestMethodOrderer(typeof(PriorityOrderer))]`. In xUnit v3 `ITestCaseOrderer` no longer orders methods, so using it silently runs the tests in random order.
- The existing integration tests do not send a token. Once `[Authorize]` is added they will get 401, so decide whether to update them.

## Runtime setup

SQL Server on `localhost,1433` with the `sa` user. The database `skills_in_action_asp_net_authentication` must exist (create it manually); Evolve creates the tables and seed data on startup. The credentials in `appsettings.json` are development defaults, and the JWT secret added later must be replaced outside local development. Evolve only runs when `ASPNETCORE_ENVIRONMENT=Development`, which the `http` and `https` launch profiles set. Ports: `http://localhost:5034` and `https://localhost:7253`. Swagger UI is at `/swagger-ui/index.html` and Scalar at `/scalar`.

```bash
docker run -d --name sqlserver -e ACCEPT_EULA=Y -e 'MSSQL_SA_PASSWORD=@Admin123' -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest
```

The e-mail settings read `EMAIL_USERNAME` and `EMAIL_PASSWORD` (names only, in the `Email` section of `appsettings.json`). Uploaded files go to `UploadDir`.

## Architecture

- Layers: `Controllers` (V1 and V2) -> `Services` -> `Repositories` (a `GenericRepository<T>` plus `PersonRepository`) -> `Model`. DTOs live in `Data/DTO/V1` and `V2` and are mapped with Mapster.
- Versioning is by route segment (`api/[controller]/v1`, `v2`). V2 only reimplements what changed.
- HATEOAS: `HypermediaFilter` is added to the MVC filters in `Program.cs` and enriches responses through content-response enrichers; `UseHATEOASRoutes()` registers the link routes.
- Pagination: `GET api/person/v1/{sortDirection}/{pageSize}/{page}`, returning a `PagedSearchDTO<T>`.
- Import/export: `FileImporterFactory` picks the CSV or XLSX importer from the uploaded file's extension, and `FileExporterFactory` picks the exporter from the `Accept` header.
- Content negotiation: `Configurations/ContentNegotiationConfig.cs`, JSON and XML, 406 for unsupported `Accept` values.
- Swagger is Swashbuckle (`SwaggerConfig`), and Scalar (`ScalarConfig`) reads Swashbuckle's document from `/swagger/v1/swagger.json`.

## Settings that must stay

- `IsEraseDisabled = true` in `EvolveConfig`: it guarantees Evolve never runs its erase command against the database.
- `Nullable` is `disable` in the app project and `enable` in the test project (the ordered tests keep state in nullable statics).
- `CustomSchemaIds(type => type.FullName)` in `SwaggerConfig`: `PersonDTO` exists in both `V1` and `V2`.
- In `Program.cs`, put `app.UseRouting()` before `UseAuthentication()` and `UseAuthorization()`, and `UseAuthentication()` before `UseAuthorization()`. Authentication has to run before authorization so the user is known when the `[Authorize]` check happens.
- The test factory calls `config.Sources.Clear()` and loads `appsettings.Test.json`, so the test run does not read the developer's real settings.
- Evolve reads `db/migrations` and `db/dataset` relative to the working directory; the test project copies those `.sql` files to its output (`CopyToOutputDirectory="Always"`).

## Package versions

Every `PackageReference` has an explicit version, the latest stable one when the project was updated, edited by hand in the `.csproj`. Never copy versions from another project. Evolve stays on `3.3.0-alpha1`: the latest stable (3.2.0) is older and it must not be downgraded. Swashbuckle 10 uses Microsoft.OpenApi 2.x, so its types are in the `Microsoft.OpenApi` namespace.

## Erudio preferences

- Content types: JSON and XML
- API documentation: Swagger + Scalar
