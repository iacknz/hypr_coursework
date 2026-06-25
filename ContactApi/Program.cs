using ContactApi.Models;
using ContactApi.Services;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ContactService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// GET /contacts - List all contacts
app.MapGet("/contacts", (ContactService service) =>
{
    return Results.Ok(service.GetAll());
})
.WithName("GetAllContacts");

// GET /contacts/{id} - Get a single contact
app.MapGet("/contacts/{id}", (int id, ContactService service) =>
{
    var contact = service.GetById(id);
    return contact is null ? Results.NotFound() : Results.Ok(contact);
})
.WithName("GetContactById");

// POST /contacts - Add a new contact
app.MapPost("/contacts", (Contact contact, ContactService service) =>
{
    if (!MiniValidator.TryValidate(contact, out var errors))
        return Results.ValidationProblem(errors);

    var created = service.Add(contact);
    return Results.Created($"/contacts/{created.Id}", created);
})
.WithName("CreateContact");

// DELETE /contacts/{id} - Delete a contact
app.MapDelete("/contacts/{id}", (int id, ContactService service) =>
{
    return service.Delete(id) ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteContact");

app.Run();
