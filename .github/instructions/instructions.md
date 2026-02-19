---
applyTo: "**"
---

## Project Development Instructions

This project is designed to follow the principles of **Domain-Driven Design (DDD)**, **Clean Architecture**, and **CQRS (Command Query Responsibility Segregation)**. Below are the conventions, structure, and best practices recommended to contribute and maintain code quality.

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
- **Value Objects**: Immutable, no identity, only attributes and comparison logic.
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

---

## 5. Best Practices

- **Clear names**: Use descriptive names for classes, methods, and files.
- **Documentation**: Comment code where necessary and keep this file updated.
- **Testing**: All new code must include unit tests and, if applicable, integration tests.
- **Domain events**: Register relevant events and handle them asynchronously if possible.
- **Avoid logic in controllers**: All logic should be in the application or domain layer.
- **Do not access infrastructure directly from the domain**.

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

---

## 8. Review and Pull Requests

- All changes must go through code review.
- Include a clear description of changes and reference related issues.
- Do not mix logic changes with refactoring or formatting changes.

---

## 9. Useful Resources

- [Domain-Driven Design Reference](https://domainlanguage.com/ddd/reference/)
- [Clean Architecture by Uncle Bob](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)

---

\*\*Thank you for contributing and maintaining the quality
