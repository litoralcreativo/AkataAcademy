# Testing Guidelines

This document describes the testing strategy for AkataAcademy, including unit, integration, and API tests, and best practices for maintainable and effective test coverage.

---

## 1. Testing Strategy Overview

- **Unit tests:** Cover domain logic, value objects, services, handlers, and validators. Use xUnit (recommended), NUnit, or MSTest.
- **Integration tests:** Verify repository behavior, database interactions, and service integration. Use EF Core InMemory provider for fast, isolated tests.
- **API tests:** Validate endpoints using WebAPI.http, Postman, or ASP.NET Core TestServer.

---

## 2. Project Structure for Tests

```
/tests
  /Unit
    /Domain
    /Application
    /Infrastructure
  /Integration
    /Persistence
    /API
```

---

## 3. Unit Testing

- Test domain entities, value objects, and business rules.
- Test command/query handlers with mocks for dependencies.
- Use FluentAssertions for expressive assertions.

**Example:**

```csharp
[Fact]
public void CourseTitle_Should_Not_Allow_Empty()
{
    Assert.Throws<DomainException>(() => new CourseTitle(""));
}
```

---

## 4. Integration Testing

- Use InMemory database for repository and persistence tests.
- Test repository methods, database transactions, and event handling.

**Example:**

```csharp
[Fact]
public async Task Repository_Should_Persist_Course()
{
    var dbContext = GetInMemoryDbContext();
    var repo = new CourseRepository(dbContext);
    var course = Course.Create(...).Value;
    await repo.AddAsync(course);
    await dbContext.SaveChangesAsync();
    var loaded = await repo.GetByIdAsync(course.Id);
    loaded.Should().NotBeNull();
}
```

---

## 5. API Testing

- Use WebAPI.http or TestServer for automated endpoint tests.
- Validate request/response, status codes, and error handling.

---

## 6. Best Practices

- All tests must be written in English.
- Use descriptive test names and method names.
- Cover edge cases and business rules.
- Prefer isolated tests; avoid shared state.
- Use Arrange-Act-Assert pattern.
- Run tests in CI pipeline.

---

## 7. Useful Resources

- [xUnit Documentation](https://xunit.net/docs/getting-started/netcore/cmdline)
- [FluentAssertions](https://fluentassertions.com/)
- [EF Core InMemory Provider](https://learn.microsoft.com/en-us/ef/core/testing/in-memory)
- [ASP.NET Core Testing](https://learn.microsoft.com/en-us/aspnet/core/test/?view=aspnetcore-8.0)

---

**Keep tests maintainable, fast, and meaningful. Thank you for contributing to the quality of AkataAcademy!**
