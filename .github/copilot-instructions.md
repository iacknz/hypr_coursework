# Contact API — Copilot Instructions

## Project Overview
A .NET 10 Minimal API for managing contacts (first name, last name, address, phone number, age, post code). All backend code is in **C#**. Any UI or front-end code is in **JavaScript**.

## Tech Stack
- **.NET 10** (net10.0) with ASP.NET Core Minimal APIs
- **MiniValidation** (0.10.0) — server-side model validation
- **Microsoft.FeatureManagement.AspNetCore** (4.6.0) — feature flags
- **Microsoft.AspNetCore.OpenApi** (10.0.8) — OpenAPI docs in dev mode
- **No database** — in-memory `List<Contact>` storage only

## Project Structure


## Coding Conventions
- **Namespaces:** `ContactApi.Models`, `ContactApi.Services`
- **Service registration:** `ContactService` as singleton (`AddSingleton<ContactService>()`)
- **Minimal API style** — no controllers, use `MapGet`/`MapPost`/`MapPut`/`MapDelete`
- **Validation:** Data annotations on model + `MiniValidator.TryValidate()` at endpoint level
- **Feature flags:** Constants in `FeatureFlags.cs`, toggled in `appsettings.json` under `"FeatureManagement"`, checked via `IFeatureManager`

## API Versioning
- **Legacy routes:** `/contacts` — backward compatible, always on
- **v1 routes:** `/v1/contacts` — stable baseline, always on
- **v2 routes:** `/v2/contacts` — enhanced version, behind `EnableV2Api` flag
- When adding new endpoints, add them to both v1 and v2 route groups where appropriate

## Feature Flags
| Flag | Controls | Default |
|------|----------|---------|
| `EnableUpdateEndpoint` | PUT endpoint on v1 | false |
| `EnableSearchEndpoint` | Search on v2 | false |
| `EnableV2Api` | All v2 endpoints | false |

## Contact Model Validation Rules
| Field | Rules |
|-------|-------|
| FirstName | Required, max 20 chars |
| LastName | Required, max 20 chars |
| Address | Required, max 50 chars |
| PhoneNumber | Required, exactly 11 digits (regex: `^\d{11}$`) |
| Age | Required, range 0–999 |
| PostCode | Required, exactly 4 digits (regex: `^\d{4}$`) |

## What NOT to Do
- Do not add a database or EF Core without asking
- Do not refactor to controller-based APIs
- Do not remove legacy `/contacts` routes
- Do not add PUT/PATCH to legacy routes
- Do not change the in-memory storage pattern without explicit request


