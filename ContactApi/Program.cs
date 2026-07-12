using ContactApi;
using ContactApi.Models;
using ContactApi.Services;
using Microsoft.FeatureManagement;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ContactService>();
builder.Services.AddOpenApi();
builder.Services.AddFeatureManagement();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// ──────────────────────────────────────────────
// v1 API — stable, always available
// ──────────────────────────────────────────────
var v1 = app.MapGroup("/v1/contacts");

// GET /v1/contacts - List all contacts
v1.MapGet("/", (ContactService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithName("GetAllContactsV1");

// GET /v1/contacts/{id} - Get a single contact
v1.MapGet("/{id}", (int id, ContactService service) =>
{
    var contact = service.GetById(id);
    return contact is null ? Results.NotFound() : Results.Ok(contact);
})
.WithName("GetContactByIdV1");

// POST /v1/contacts - Add a new contact
v1.MapPost("/", (Contact contact, ContactService service) =>
{
    if (!MiniValidator.TryValidate(contact, out var errors))
        return Results.ValidationProblem(errors);

    var created = service.Add(contact);
    return Results.Created($"/v1/contacts/{created.Id}", created);
})
.WithName("CreateContactV1");

// DELETE /v1/contacts/{id} - Delete a contact
v1.MapDelete("/{id}", (int id, ContactService service) =>
{
    return service.Delete(id) ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteContactV1");

// PUT /v1/contacts/{id} - Update a contact (behind feature flag)
v1.MapPut("/{id}", async (int id, Contact contact, ContactService service, IFeatureManager features) =>
{
    if (!await features.IsEnabledAsync(FeatureFlags.EnableUpdateEndpoint))
        return Results.NotFound();

    if (!MiniValidator.TryValidate(contact, out var errors))
        return Results.ValidationProblem(errors);

    var updated = service.Update(id, contact);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
})
.WithName("UpdateContactV1");

// ──────────────────────────────────────────────
// v2 API — enhanced version (behind feature flag)
// ──────────────────────────────────────────────
var v2 = app.MapGroup("/v2/contacts");

// GET /v2/contacts - List all contacts (v2)
v2.MapGet("/", (ContactService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithName("GetAllContactsV2");

// GET /v2/contacts/{id} - Get a single contact (v2)
v2.MapGet("/{id}", (int id, ContactService service) =>
{
    var contact = service.GetById(id);
    return contact is null ? Results.NotFound() : Results.Ok(contact);
})
.WithName("GetContactByIdV2");

// POST /v2/contacts - Add a new contact (v2)
v2.MapPost("/", (Contact contact, ContactService service) =>
{
    if (!MiniValidator.TryValidate(contact, out var errors))
        return Results.ValidationProblem(errors);

    var created = service.Add(contact);
    return Results.Created($"/v2/contacts/{created.Id}", created);
})
.WithName("CreateContactV2");

// PUT /v2/contacts/{id} - Update a contact (v2)
v2.MapPut("/{id}", (int id, Contact contact, ContactService service) =>
{
    if (!MiniValidator.TryValidate(contact, out var errors))
        return Results.ValidationProblem(errors);

    var updated = service.Update(id, contact);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
})
.WithName("UpdateContactV2");

// DELETE /v2/contacts/{id} - Delete a contact (v2)
v2.MapDelete("/{id}", (int id, ContactService service) =>
{
    return service.Delete(id) ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteContactV2");

// GET /v2/contacts/search?q= - Search contacts by name (behind feature flag)
v2.MapGet("/search", async (string? q, ContactService service, IFeatureManager features) =>
{
    if (!await features.IsEnabledAsync(FeatureFlags.EnableSearchEndpoint))
        return Results.NotFound();

    if (string.IsNullOrWhiteSpace(q))
        return Results.Ok(service.GetAll());

    var results = service.GetAll()
        .Where(c => c.FirstName.Contains(q, StringComparison.OrdinalIgnoreCase)
                 || c.LastName.Contains(q, StringComparison.OrdinalIgnoreCase))
        .ToList();

    return Results.Ok(results);
})
.WithName("SearchContactsV2");

// ──────────────────────────────────────────────
// Legacy routes — redirect /contacts to /v1/contacts
// ──────────────────────────────────────────────
app.MapGet("/contacts", (ContactService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithName("GetAllContactsLegacy");

app.MapGet("/contacts/{id}", (int id, ContactService service) =>
{
    var contact = service.GetById(id);
    return contact is null ? Results.NotFound() : Results.Ok(contact);
})
.WithName("GetContactByIdLegacy");

app.MapPost("/contacts", (Contact contact, ContactService service) =>
{
    if (!MiniValidator.TryValidate(contact, out var errors))
        return Results.ValidationProblem(errors);

    var created = service.Add(contact);
    return Results.Created($"/contacts/{created.Id}", created);
})
.WithName("CreateContactLegacy");

app.MapDelete("/contacts/{id}", (int id, ContactService service) =>
{
    return service.Delete(id) ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteContactLegacy");

app.Run();
