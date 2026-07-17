# PaymentProcessing

PaymentProcessing is a .NET 10 solution that implements payment processing components (Web API, Application, Domain, DataAccess) and an IntegrationTests project.

## Prerequisites
- .NET 10 SDK
- Git
- Visual Studio 2026 (optional) or any editor/IDE that supports .NET 10

## Projects
- PaymentProcessing.Web - ASP.NET Web project
- PaymentProcessing.Application - Application layer and services
- PaymentProcessing.Domain - Domain models and interfaces
- PaymentProcessing.DataAccess - Persistence layer
- PaymentProcessing.IntegrationTests - Integration tests

## Build
From the repository root:

dotnet build PaymentProcessing.slnx

## Run
To run the web project locally:

dotnet run --project PaymentProcessing.Web/PaymentProcessing.Web.csproj

## Tests
Run all tests:

dotnet test

Run only integration tests project:

dotnet test PaymentProcessing.IntegrationTests/PaymentProcessing.IntegrationTests.csproj

## Contributing
Contributions welcome. Open issues or pull requests against the `main` branch.

## License
See repository license (if any).

## Readme change
