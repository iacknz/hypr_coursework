# About — Contact API

## Project Description
A simple .NET 10 Minimal API for managing contacts. It provides CRUD operations (Create, Read, Delete) for contact records stored in memory. The API is designed to be lightweight, easy to understand, and suitable for small-scale contact management needs. All backend code is written in **C#**; any UI or front-end code is written in **JavaScript**.

## Problem to Solve
Users need a straightforward way to store and retrieve contact information (first name, last name, address, phone number, age, post code) without the overhead of a full database system. The API should validate input data to ensure data integrity and provide clear feedback when validation fails.

## User Personas
- **Developer / Integrator** — Wants to integrate contact management into a larger system via RESTful HTTP calls. Needs clear endpoints, predictable responses, and proper HTTP status codes.
- **End User (via client app)** — Uses an application that consumes this API to manage their contacts. Expects reliable storage and validation of their contact data.
- **Learner / Hobbyist** — Studies the codebase to learn .NET Minimal APIs, in-memory services, and model validation patterns.

## Constraints
- **No database** — All data is stored in-memory using a `List<Contact>`. Data is lost when the application restarts.
- **Singleton service** — `ContactService` is registered as a singleton, meaning all requests share the same in-memory data store.
- **No update endpoint** — The API currently supports only Create, Read, and Delete operations (no PUT/PATCH for updates).
- **Validation rules** — Strict validation via data annotations and MiniValidation (e.g., phone must be exactly 11 digits, post code exactly 4 digits).
- **.NET 10 only** — Targets `net10.0` and uses ASP.NET Core Minimal APIs.

## Contact Model

| Field       | Type   | Validation                                      |
|-------------|--------|--------------------------------------------------|
| Id          | int    | Auto-generated                                   |
| FirstName   | string | Required, max 20 chars                           |
| LastName    | string | Required, max 20 chars                           |
| Address     | string | Required, max 50 chars                           |
| PhoneNumber | string | Required, exactly 11 digits (regex: `^\d{11}$`)  |
| Age         | int    | Required, range 0–999                            |
| PostCode    | string | Required, exactly 4 digits (regex: `^\d{4}$`)    |