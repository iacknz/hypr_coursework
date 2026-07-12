# Implementation — Contact API

## Language
- **C#** — All application code is written in C#.
- **Minimal API syntax** — Uses top-level statements, lambda route handlers, and implicit usings.
- **JavaScript** — Any future UI or front-end client would be written in JavaScript.

## Versions
| Component | Version |
|-----------|---------|
| .NET SDK | **10.0** (target framework: `net10.0`) |
| ASP.NET Core | **10.0** (included in .NET 10 SDK) |
| C# Language | **14** (default for .NET 10) |
| Microsoft.AspNetCore.OpenApi | **10.0.8** |
| MiniValidation | **0.10.0** |
| Microsoft.FeatureManagement.AspNetCore | **4.6.0** |

## External Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.AspNetCore.OpenApi` | 10.0.8 | Enables OpenAPI document generation in development mode (`/openapi/v1.json`) |
| `MiniValidation` | 0.10.0 | Lightweight server-side model validation using data annotations — invoked via `MiniValidator.TryValidate()` |
| `Microsoft.FeatureManagement.AspNetCore` | 4.6.0 | Feature flag system — allows toggling endpoints on/off via `appsettings.json` without code changes |

## Project Configuration
- **SDK:** `Microsoft.NET.Sdk.Web`
- **Nullable:** Enabled (nullable reference types)
- **ImplicitUsings:** Enabled (common namespaces like `System`, `System.Linq`, `Microsoft.AspNetCore.Builder` are auto-imported)
- **No database** — Pure in-memory storage, no EF Core, no SQL, no external data stores.

## Conventions
- **Namespaces:** `ContactApi.Models` for entities, `ContactApi.Services` for services
- **Service registration:** `ContactService` is registered as a singleton (`AddSingleton<ContactService>()`)
- **Storage:** In-memory `List<Contact>` with auto-incrementing integer IDs
- **API Versioning:** Endpoints are grouped by version path (`/v1/`, `/v2/`). Legacy `/contacts` routes maintained for backward compatibility.
- **Feature Flags:** Defined as constants in `FeatureFlags.cs`, toggled in `appsettings.json` under `"FeatureManagement"`. Evaluated at runtime via `IFeatureManager`.