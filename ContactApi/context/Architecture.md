# Architecture — Contact API

## Patterns
- **Minimal API** — Uses ASP.NET Core Minimal API host model (`WebApplication.CreateBuilder`) with lambda-based route handlers instead of controllers.
- **Backend / Frontend Split** — All backend logic is written in **C#** (API, models, services). Any UI or client-side code is written in **JavaScript**.
- **In-Memory Repository** — `ContactService` acts as a simple in-memory repository, storing contacts in a `List<Contact>` with auto-incrementing IDs.
- **Singleton Service** — `ContactService` is registered as a singleton via `AddSingleton<ContactService>()`, ensuring a single shared data store across all requests.
- **Data Annotations Validation** — Model validation is handled declaratively via `System.ComponentModel.DataAnnotations` attributes on the `Contact` class.
- **MiniValidation** — The `MiniValidator.TryValidate()` utility is used at the API endpoint level to trigger validation and return structured error responses.

## Modules

| Module | File | Responsibility |
|--------|------|----------------|
| **Models** | `Models/Contact.cs` | Defines the `Contact` entity with data annotation validation attributes |
| **Services** | `Services/ContactService.cs` | In-memory CRUD logic — GetAll, GetById, Add, Delete |
| **API Layer** | `Program.cs` | Route definitions, request handling, validation invocation, response formatting |
| **Configuration** | `appsettings.json` / `appsettings.Development.json` | App configuration (environment, logging, etc.) |
| **OpenAPI** | (via middleware) | Auto-generated OpenAPI docs in development mode |

## Relationships
```
Client (HTTP request)
       │
       ▼
  Program.cs ────► ContactService ────► List<Contact> (in-memory)
       │                │
       │                └── Uses Contact model
       │
       └── MiniValidator.TryValidate() ────► Contact (data annotations)
```

- `Program.cs` depends on `ContactService` (injected via DI) and `Contact` model.
- `ContactService` depends on `Contact` model.
- `MiniValidation` library validates `Contact` instances against their data annotations.

## Dataflow

### GET /contacts (List All)
```
Request ──► MapGet("/contacts") ──► service.GetAll() ──► return 200 OK + JSON array
```

### GET /contacts/{id} (Get by ID)
```
Request ──► MapGet("/contacts/{id}") ──► service.GetById(id)
    ├── Found? ──► 200 OK + JSON contact
    └── Not found? ──► 404 NotFound
```

### POST /contacts (Create)
```
Request ──► MapPost("/contacts") ──► MiniValidator.TryValidate(contact)
    ├── Invalid? ──► 400 ValidationProblem (error details)
    └── Valid? ──► service.Add(contact) ──► 201 Created + location header + JSON
```

### DELETE /contacts/{id} (Delete)
```
Request ──► MapDelete("/contacts/{id}") ──► service.Delete(id)
    ├── Deleted? ──► 204 NoContent
    └── Not found? ──► 404 NotFound
```

## API Endpoints

| Method | Route           | Description         | Returns                |
|--------|-----------------|---------------------|------------------------|
| GET    | /contacts       | List all contacts   | 200 OK + JSON array    |
| GET    | /contacts/{id}  | Get contact by ID   | 200 OK or 404          |
| POST   | /contacts       | Create a contact    | 201 Created or 400     |
| DELETE | /contacts/{id}  | Delete a contact    | 204 No Content or 404  |

Validation uses `MiniValidator.TryValidate()` — invalid models return 400 with validation errors.