# Architecture — Contact API

## Patterns
- **Minimal API** — Uses ASP.NET Core Minimal API host model (`WebApplication.CreateBuilder`) with lambda-based route handlers instead of controllers.
- **Backend / Frontend Split** — All backend logic is written in **C#** (API, models, services). Any UI or client-side code is written in **JavaScript**.
- **API Versioning** — Endpoints are grouped by version (`/v1/contacts`, `/v2/contacts`). v1 is the stable baseline; v2 adds enhancements. Legacy `/contacts` routes are maintained for backward compatibility.
- **Feature Flags** — New or experimental features are hidden behind toggles in `appsettings.json`. A feature must be explicitly turned on before its endpoint becomes available. Uses `Microsoft.FeatureManagement`.
- **In-Memory Repository** — `ContactService` acts as a simple in-memory repository, storing contacts in a `List<Contact>` with auto-incrementing IDs.
- **Singleton Service** — `ContactService` is registered as a singleton via `AddSingleton<ContactService>()`, ensuring a single shared data store across all requests.
- **Data Annotations Validation** — Model validation is handled declaratively via `System.ComponentModel.DataAnnotations` attributes on the `Contact` class.
- **MiniValidation** — The `MiniValidator.TryValidate()` utility is used at the API endpoint level to trigger validation and return structured error responses.

## Modules

| Module | File | Responsibility |
|--------|------|----------------|
| **Models** | `Models/Contact.cs` | Defines the `Contact` entity with data annotation validation attributes |
| **Services** | `Services/ContactService.cs` | In-memory CRUD logic — GetAll, GetById, Add, Update, Delete |
| **API Layer** | `Program.cs` | Route definitions (v1, v2, legacy), request handling, validation invocation, response formatting |
| **Feature Flags** | `FeatureFlags.cs` | Central list of feature flag constant names used across the application |
| **Configuration** | `appsettings.json` / `appsettings.Development.json` | App configuration including feature flag toggles |
| **OpenAPI** | (via middleware) | Auto-generated OpenAPI docs in development mode |

## Relationships
```
Client (HTTP request)
       │
       ▼
  Program.cs ────► Feature Flags ────► ContactService ────► List<Contact> (in-memory)
       │                │                     │
       │                │                     └── Uses Contact model
       │                │
       │                ├── EnableUpdateEndpoint (v1 PUT)
       │                ├── EnableSearchEndpoint (v2 search)
       │                └── EnableV2Api (all v2 routes)
       │
       └── MiniValidator.TryValidate() ────► Contact (data annotations)
```

- `Program.cs` depends on `ContactService`, `IFeatureManager`, and `Contact` model.
- `ContactService` depends on `Contact` model.
- `FeatureFlags.cs` provides constant names referenced by `Program.cs` and `appsettings.json`.
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

### PUT /contacts/{id} (Update — behind feature flag)
```
Request ──► MapPut("/contacts/{id}") ──► Feature flag ON?
    ├── Flag OFF? ──► 404 NotFound (feature hidden)
    └── Flag ON? ──► MiniValidator.TryValidate(contact)
         ├── Invalid? ──► 400 ValidationProblem
         └── Valid? ──► service.Update(id, contact)
              ├── Not found? ──► 404 NotFound
              └── Updated? ──► 200 OK + JSON
```

### GET /v2/contacts/search?q= (Search — behind feature flag)
```
Request ──► MapGet("/v2/contacts/search") ──► Feature flags ON?
    ├── Any flag OFF? ──► 404 NotFound
    └── Both ON? ──► Filter contacts by name
         ├── No query? ──► 200 OK + all contacts
         └── Has query? ──► 200 OK + matching contacts
```

## API Endpoints

### Legacy Routes (backward compatibility — `/contacts`)

| Method | Route | Description | Returns |
|--------|-------|-------------|---------|
| GET | `/contacts` | List all contacts | 200 OK + JSON array |
| GET | `/contacts/{id}` | Get contact by ID | 200 OK or 404 |
| POST | `/contacts` | Create a contact | 201 Created or 400 |
| DELETE | `/contacts/{id}` | Delete a contact | 204 No Content or 404 |

### v1 API (stable — `/v1/contacts`)

| Method | Route | Description | Returns | Feature Flag |
|--------|-------|-------------|---------|-------------|
| GET | `/v1/contacts` | List all contacts | 200 OK + JSON array | Always on |
| GET | `/v1/contacts/{id}` | Get contact by ID | 200 OK or 404 | Always on |
| POST | `/v1/contacts` | Create a contact | 201 Created or 400 | Always on |
| PUT | `/v1/contacts/{id}` | Update a contact | 200 OK or 404 | `EnableUpdateEndpoint` |
| DELETE | `/v1/contacts/{id}` | Delete a contact | 204 No Content or 404 | Always on |

### v2 API (enhanced — `/v2/contacts`)

| Method | Route | Description | Returns | Feature Flag |
|--------|-------|-------------|---------|-------------|
| GET | `/v2/contacts` | List all contacts | 200 OK + JSON array | `EnableV2Api` |
| GET | `/v2/contacts/{id}` | Get contact by ID | 200 OK or 404 | `EnableV2Api` |
| POST | `/v2/contacts` | Create a contact | 201 Created or 400 | `EnableV2Api` |
| PUT | `/v2/contacts/{id}` | Update a contact | 200 OK or 404 | `EnableV2Api` |
| DELETE | `/v2/contacts/{id}` | Delete a contact | 204 No Content or 404 | `EnableV2Api` |
| GET | `/v2/contacts/search?q=` | Search contacts by name | 200 OK + JSON array | `EnableV2Api` + `EnableSearchEndpoint` |

Validation uses `MiniValidator.TryValidate()` — invalid models return 400 with validation errors.