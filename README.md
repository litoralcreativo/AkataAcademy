# AkataAcademy

AkataAcademy is an educational platform developed in .NET 8 following the principles of DDD (Domain-Driven Design), Clean Architecture, and CQRS. The goal is to provide a robust, scalable, and maintainable foundation for modern educational applications.

## Table of Contents

- [AkataAcademy](#akataacademy)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Architecture](#architecture)
  - [Project Structure](#project-structure)
  - [Technologies Used](#technologies-used)
  - [Installation and Running](#installation-and-running)
  - [Testing](#testing)
  - [API Usage](#api-usage)
  - [CQRS + Domain Events Sequence Diagram (by stages)](#cqrs--domain-events-sequence-diagram-by-stages)
    - [1. Input and Dispatch](#1-input-and-dispatch)
    - [2. Persistence and UnitOfWork](#2-persistence-and-unitofwork)
    - [3. Domain Events and EventBus](#3-domain-events-and-eventbus)
      - [Additional Notes](#additional-notes)
  - [Layers and Communication Diagram](#layers-and-communication-diagram)
  - [Conventions and Best Practices](#conventions-and-best-practices)
  - [Contributions](#contributions)
  - [License](#license)

---

## Features

- Clean and decoupled architecture ([DDD](https://martinfowler.com/bliki/DomainDrivenDesign.html) + [CQRS](https://martinfowler.com/bliki/CQRS.html) + [Clean Architecture](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html))
- Clear separation of layers: Domain, Application, Infrastructure, and Presentation
- Use of [Value Objects](https://martinfowler.com/bliki/ValueObject.html) and [Entities](https://martinfowler.com/bliki/EvansClassification.html)
- Dependency injection without [MediatR](https://github.com/jbogard/MediatR)
- Minimal API endpoints and traditional controllers
- HTTP tests included for main endpoints
- Flexible [EF Core](https://learn.microsoft.com/en-us/ef/core/) configuration (InMemory, SQL Server, PostgreSQL)

## Architecture

The project follows Clean Architecture, with a clear separation of responsibilities into four main layers: Domain, Application, Infrastructure, and Presentation. For a detailed description of each layer and their responsibilities, see the [Project Structure](#project-structure) section below.

## Project Structure

```
AkataAcademy.sln
├── Application/
│   ├── AkataAcademy.Application.csproj
│   ├── Catalog/
│   │   ├── Commands/
│   │   ├── DTOs/
│   │   ├── Queries/
│   └── Common/
├── Domain/
│   ├── AkataAcademy.Domain.csproj
│   ├── BoundedContexts/
│   │   ├── Catalog/
│   │   ├── Certification/
│   │   └── Enrollment/
│   └── Common/
├── Infrastructure/
│   ├── AkataAcademy.Infrastructure.csproj
│   ├── Messaging/
│   └── Persistence/
│       ├── Configurations/
│       └── Repositories/
└── Presentation/
    ├── AkataAcademy.Presentation.csproj
    ├── Controllers/
    ├── appsettings.json
    └── WebAPI.http
```

**Layered Overview:**

- **Application:** Use cases, commands, queries, DTOs, and handler registration.
- **Domain:** Core business logic, entities, value objects, and domain events, organized by bounded context.
- **Infrastructure:** Persistence (EF Core), messaging, repository implementations, and configurations.
- **Presentation:** API controllers, minimal API endpoints, configuration files, and HTTP test scripts.

## Technologies Used

- .NET 8
- Entity Framework Core 8.x
- InMemory Database Provider (development/test)
- CQRS (Command Query Responsibility Segregation)
- DDD (Domain-Driven Design)
- Minimal API
- Value Objects
- Dependency Injection

## Installation and Running

1. **Clone the repository:**
   ```bash
   git clone https://github.com/litoralcreativo/AkataAcademy.git
   cd AkataAcademy
   ```
2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```
3. **Build the solution:**
   ```bash
   dotnet build
   ```
4. **Run the API:**
   ```bash
   dotnet run --project Presentation/AkataAcademy.Presentation.csproj
   ```
5. **Test the endpoints:**
   Use the `WebAPI.http` file to test the main endpoints from VS Code or tools like Postman.

## Testing

- The `WebAPI.http` file contains examples of GET and POST requests to test the API.
- You can add unit tests in the future using xUnit, NUnit, or MSTest.

## API Usage

- **GET /api/catalog?includeUnpublished=true|false**: Gets published courses or all courses depending on the flag.
- **POST /api/catalog**: Creates a new course. Example payload:
  ```json
  {
    "title": "Understanding Software Architecture",
    "description": "POC of Domain-Driven Design, CQRS, and Clean Architecture"
  }
  ```

## CQRS + Domain Events Sequence Diagram (by stages)

### 1. Input and Dispatch

```mermaid
sequenceDiagram
    participant Client
    participant Presentation
    participant CommandDispatcher
    participant CommandHandler

    Client->>Presentation: HTTP Request (POST/PUT/DELETE)
    Presentation->>CommandDispatcher: Sends command
    CommandDispatcher->>CommandHandler: Resolves and executes handler
```

### 2. Persistence and UnitOfWork

```mermaid
sequenceDiagram
    participant CommandHandler
    participant Repository
    participant UnitOfWork
    participant DB as Database

    CommandHandler->>Repository: Modifies/adds entity
    Repository->>UnitOfWork: Calls SaveChangesAsync()
    UnitOfWork->>DB: Persists changes
    Note right of UnitOfWork: Detects entities with Domain Events
```

### 3. Domain Events and EventBus

```mermaid
sequenceDiagram
    participant UnitOfWork
    participant DomainEventDispatcher
    participant DomainEventHandler
    participant EventBus
    participant IntegrationEventHandler

    UnitOfWork->>DomainEventDispatcher: Dispatches Domain Events
    DomainEventDispatcher->>DomainEventHandler: Processes domain event
    DomainEventHandler-->>EventBus: (Optional) Publishes IntegrationEvent
    EventBus-->>IntegrationEventHandler: Notifies other BCs/systems
```

#### Additional Notes

- **EventBus** can be in-memory, RabbitMQ, Azure Service Bus, etc.
- **IntegrationEventHandler** can be in other bounded contexts or microservices.
- The flow is fully asynchronous and decoupled.
- You can have multiple IntegrationEventHandlers for the same event, each in a different context.

## Layers and Communication Diagram

```mermaid
graph TD

    subgraph Presentation
        direction LR
        A --> Acd
        A --> Aqd
        A@{ shape: rounded, label: "Web API" }
        Acd@{ shape: rounded, label: "ICommandDipatcher" }
        Aqd@{ shape: rounded, label: "IQueryDipatcher" }

    end

    Acd e1@--> ICD
    Aqd e2@--> IQD
    e1@{ animation: fast }
    e2@{ animation: fast }

    subgraph Domain
        D@{ shape: rounded, label: "Entities" }
        DE@{ shape: rounded, label: "DomainEvents" }

        D --> DE
    end

    subgraph Application

        IQD@{ shape: rounded, label: "QueryDipatcher" }
        IQH@{ shape: rounded, label: "IQueryHandler" }
        QH@{ shape: rounded, label: "QueryHandler" }

        ICD@{ shape: rounded, label: "CommandDipatcher" }
        ICH@{ shape: rounded, label: "ICommandHandler" }
        CH@{ shape: rounded, label: "CommandHandler" }

        DEH@{ shape: rounded, label: "DomainEventHandler" }
        IEB@{ shape: rounded, label: "IEventBus" }
        IEH@{ shape: rounded, label: "IntegrationEventHandler" }

        IRR@{ shape: rounded, label: "IReadRepository" }
        IR@{ shape: rounded, label: "IRepository" }

        IUOW@{ shape: rounded, label: "IUnitOfWork" }

        IQD ==> IQH
        ICD ==> ICH
        ICH --> CH
        IQH --> QH
        QH --> IRR
        CH --> IR
        CH --> IUOW
        DEH --> IEB
    end
    IR --> D

    IRR e3@--> RR
    e3@{ animation: fast }
    IR e4@--> R
    e4@{ animation: fast }
    IUOW e5@--> UOW
    e5@{ animation: fast }


    subgraph Infrastructure
        subgraph Repositories
        end
            RR@{ shape: rounded, label: "ReadRepository" }
            R@{ shape: rounded, label: "Repository" }
        UOW@{ shape: rounded, label: "ApplicationDbContext" }
        IDED@{ shape: rounded, label: "IDomainEventDispatcher" }
        DED@{ shape: rounded, label: "DomainEventDispatcher" }
        IDEH@{ shape: rounded, label: "IDomainEventHandler" }
        EB@{ shape: rounded, label: "EventBus" }
        IIEH@{ shape: rounded, label: "IIntegrationEventHandler" }

        UOW --> IDED
        IDED --> DED
        DED ==> IDEH
        EB ==> IIEH
    end

    IDEH e6@--> DEH
    e6@{ animation: fast }
    IEB e7@--> EB
    e7@{ animation: fast }
    IIEH e8@--> IEH
    e8@{ animation: fast }
```

## Conventions and Best Practices

- Value Objects are mapped using `OwnsOne` in EF Core.
- Command and query handlers are registered automatically by reflection.
- MediatR is not used; handlers are resolved via DI.
- It is recommended to maintain layer separation and avoid circular dependencies.

## Contributions

Contributions are welcome! Please open an issue or pull request following the project's best practices.

## License

This project is under the MIT license. See the LICENSE.txt file for more details.
