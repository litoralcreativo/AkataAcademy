# AkataAcademy

AkataAcademy es una plataforma educativa desarrollada en .NET 8 siguiendo los principios de DDD (Domain-Driven Design), Clean Architecture y CQRS. El objetivo es ofrecer una base robusta, escalable y mantenible para aplicaciones educativas modernas.

## Tabla de Contenidos

- [AkataAcademy](#akataacademy)
  - [Tabla de Contenidos](#tabla-de-contenidos)
  - [Características](#características)
  - [Arquitectura](#arquitectura)
  - [Estructura del Proyecto](#estructura-del-proyecto)
  - [Tecnologías Utilizadas](#tecnologías-utilizadas)
  - [Instalación y Ejecución](#instalación-y-ejecución)
  - [Testing](#testing)
  - [Uso de la API](#uso-de-la-api)
  - [Convenciones y Buenas Prácticas](#convenciones-y-buenas-prácticas)
  - [Contribuciones](#contribuciones)
  - [Licencia](#licencia)

---

## Características

- Arquitectura limpia y desacoplada (DDD + CQRS + Clean Architecture)
- Separación clara de capas: Dominio, Aplicación, Infraestructura y Presentación
- Uso de Value Objects y Entities
- Inyección de dependencias sin MediatR
- Endpoints minimal API y controladores tradicionales
- Pruebas HTTP incluidas para endpoints principales
- Configuración flexible de EF Core (InMemory, SQL Server, PostgreSQL)

## Arquitectura

El proyecto sigue Clean Architecture, separando responsabilidades en capas:

- **Domain**: Lógica de negocio, entidades, value objects, interfaces de repositorio
- **Application**: Casos de uso, comandos, queries, DTOs, interfaces de handlers
- **Infrastructure**: Implementaciones de persistencia, configuración de EF Core, mensajería
- **Presentation**: API HTTP (controladores y/o minimal API), configuración de DI, archivos de prueba HTTP

## Estructura del Proyecto

```
AkataAcademy.sln
Application/
  AkataAcademy.Application.csproj
  Catalog/
    Commands/
    DTOs/
    Queries/
  Common/
Domain/
  AkataAcademy.Domain.csproj
  BoundedContexts/
    Catalog/
    Certification/
    Enrollment/
  Common/
Infrastructure/
  AkataAcademy.Infrastructure.csproj
  Messaging/
  Persistence/
    Configurations/
    Repositories/
Presentation/
  AkataAcademy.Presentation.csproj
  Controllers/
  appsettings.json
  WebAPI.http
```

## Tecnologías Utilizadas

- .NET 8
- Entity Framework Core 8.x
- InMemory Database Provider (desarrollo/test)
- CQRS (Command Query Responsibility Segregation)
- DDD (Domain-Driven Design)
- Minimal API
- Value Objects
- Dependency Injection

## Instalación y Ejecución

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/litoralcreativo/AkataAcademy.git
   cd AkataAcademy
   ```
2. **Restaura los paquetes NuGet:**
   ```bash
   dotnet restore
   ```
3. **Compila la solución:**
   ```bash
   dotnet build
   ```
4. **Ejecuta la API:**
   ```bash
   dotnet run --project Presentation/AkataAcademy.Presentation.csproj
   ```
5. **Prueba los endpoints:**
   Utiliza el archivo `WebAPI.http` para probar los endpoints principales desde VS Code o herramientas como Postman.

## Testing

- El archivo `WebAPI.http` contiene ejemplos de peticiones GET y POST para probar la API.
- Puedes agregar pruebas unitarias en el futuro usando xUnit, NUnit o MSTest.

## Uso de la API

- **GET /api/catalog?includeUnpublished=true|false**: Obtiene cursos publicados o todos según la bandera.
- **POST /api/catalog**: Crea un nuevo curso. Ejemplo de payload:
  ```json
  {
    "title": "Entendiendo arquitectura de software",
    "description": "POC de Domain-Driven Design, CQRS y Clean Architecture"
  }
  ```

## Convenciones y Buenas Prácticas

- Value Objects se mapean usando `OwnsOne` en EF Core.
- Los handlers de comandos y queries se registran automáticamente por reflexión.
- No se utiliza MediatR, los handlers se resuelven vía DI.
- Se recomienda mantener la separación de capas y evitar dependencias circulares.

## Contribuciones

¡Las contribuciones son bienvenidas! Por favor, abre un issue o pull request siguiendo las buenas prácticas del proyecto.

## Licencia

Este proyecto está bajo la licencia MIT. Consulta el archivo LICENSE.txt para más detalles.
