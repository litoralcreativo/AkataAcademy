---
applyTo: "**"
---

## Project Development Instructions

For business context and domain rules, see domain-context.md.

This project is designed to follow the principles of **Domain-Driven Design (DDD)**, **Clean Architecture**, and **CQRS (Command Query Responsibility Segregation)**. Below are the conventions, structure, and best practices recommended to contribute and maintain code quality.

---

## Language and Documentation Policy

- All generated code, comments, documentation, and commit messages must be written in English. No exceptions.
- Any code, documentation, or comments submitted in another language will be rejected during review.

---

## 1. Project Structure

The solution must be organized into clearly separated layers:

```
/src
  /Domain
    /BoundedContexts
      /[ContextName]
        /Entities
        /ValueObjects
        /Events
        /Repositories
        /Specifications
  /Application
    /[ContextName]
      /Commands
      /Queries
      /DTOs
      /Handlers
      /Validators
      /Services
  /Infrastructure
    /Persistence
    /Repositories
    /Migrations
    /ExternalServices
  /API
    /Controllers
    /Models
    /Filters
    /Middlewares
/tests
  /Unit
  /Integration
```

---

## 2. DDD Principles

- **Entities**: Must have identity and relevant business logic.
- **Value Objects**: Immutable, no identity, only attributes and comparison logic. Use C# records and provide protected parameterless constructors for EF Core compatibility.
- **Aggregates**: Control consistency of their internal entities and value objects.
- **Domain Events**: Use to communicate important changes within the domain.
- **Repositories**: Only expose necessary methods for the aggregate root.

---

## 3. Clean Architecture

- **Layer separation**: The domain does not depend on any other layer.
- **Dependency inversion**: Use interfaces to abstract infrastructure and external services.
- **Dependency injection**: Configure at the application entry point (e.g., Startup.cs).

---

## 4. CQRS

- **Commands**: Modify system state. Do not return complex data, only confirmation.
- **Queries**: Read-only. Do not modify state, return DTOs or query models.
- **Handlers**: Each command or query has its own handler.
- **Validation**: Use validators (e.g., FluentValidation) for commands and queries.
- **Handlers for collections**: Always return Success with an empty list if no elements are found. Never return an error for empty collections.

---

## 5. Best Practices

- **Credential management**: Never store sensitive credentials in appsettings.json or source code. Use `dotnet user-secrets` for local development and environment variables for production.
- **Database migrations**: Document migration commands in README. Always create and apply migrations after model changes. Use EF Core tools for migration management.
- **Switching database providers**: For quick testing, use the InMemory provider. Comment/uncomment the relevant lines in DependencyInjection.cs as described in README.
- **Clear names**: Use descriptive names for classes, methods, and files.
- **Documentation**: Comment code where necessary and keep this file updated. All documentation must be in English.
- **Testing**: All new code must include unit tests and, if applicable, integration tests. Use InMemory database for fast tests.
- **Domain events**: Register relevant events and handle them asynchronously if possible.
- **Avoid logic in controllers**: All logic should be in the application or domain layer.
- **Do not access infrastructure directly from the domain**.
- **REST API conventions**: Controllers must translate Result objects to appropriate ActionResult responses. For collection queries, always return 200 OK with an empty list if no elements are found. Only return 404 for single resource queries when the resource does not exist.

---

## 5.1 Database Configuration and Testing

- **PostgreSQL setup**: Use Npgsql provider and store connection string securely with user-secrets.
- **InMemory for testing**: Switch to InMemory provider for local or CI tests by commenting/uncommenting lines in DependencyInjection.cs. See README for details.
- **Migration workflow**: After changing the model, run:
  ```bash
  dotnet ef migrations add <MigrationName> --project Infrastructure --startup-project Presentation
  dotnet ef database update --project Infrastructure --startup-project Presentation
  ```
- **Resetting migrations**: If you drop the database, delete the Migrations folder and the \_\_EFMigrationsHistory table, then create a new migration.
- **Credential tips**: List and remove user-secrets with:
  ```bash
  dotnet user-secrets list
  dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
  ```

---

## 6. CQRS Flow Example

1. **Command**: The user sends a request to create a course.
2. **Handler**: The handler validates the command and uses the domain to create the entity.
3. **Repository**: The handler persists the entity using a repository.
4. **Domain event**: A `CourseCreated` event is triggered.
5. **Query**: The user queries available courses.
6. **Query Handler**: Retrieves the data and returns it as a DTO.

---

## 7. Code Conventions

- Use 4 spaces for indentation.
- Follow C# and .NET conventions for naming and organization.
- Use `null!` in protected constructors for properties required by EF Core.
- Keep classes as small and focused as possible.
- All code, comments, and documentation must be in English.

---

## 8. Review and Pull Requests

- All changes must go through code review.
- Include a clear description of changes and reference related issues.
- Do not mix logic changes with refactoring or formatting changes.
- All review comments and discussions must be in English.

---

## 9. Useful Resources

- [Domain-Driven Design Reference](https://domainlanguage.com/ddd/reference/)
- [Clean Architecture by Uncle Bob](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

---

**Thank you for contributing and maintaining the quality of this project!**
