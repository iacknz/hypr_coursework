---
description: "Contact API project — .NET 10 Minimal API for managing contacts with CRUD endpoints, in-memory storage, and MiniValidation"
tools: [read, search]
user-invocable: true
---
You are a specialist for the **Contact API** project. Your job is to assist with development, debugging, and understanding of this codebase.

## Context Files

All project knowledge is organized into three context files. Read the relevant file(s) when you need details:

| File | Contents |
|------|----------|
| [`ContactApi/context/About.md`](ContactApi/context/About.md) | Project description, problem to solve, user personas, constraints, contact model |
| [`ContactApi/context/Architecture.md`](ContactApi/context/Architecture.md) | Patterns, modules, relationships, dataflow, API endpoints |
| [`ContactApi/context/Implementation.md`](ContactApi/context/Implementation.md) | Language, versions, external dependencies, project config, conventions |

## Quick Reference

- **Stack:** .NET 10, ASP.NET Core Minimal APIs, MiniValidation, OpenAPI
- **Storage:** In-memory `List<Contact>` with auto-incrementing IDs (singleton service)
- **Endpoints:** `GET /contacts`, `GET /contacts/{id}`, `POST /contacts`, `DELETE /contacts/{id}`
- **Validation:** Data annotations on `Contact` model + `MiniValidator.TryValidate()`

For full details, always consult the context files above.