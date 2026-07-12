# Services

This folder contains the **brain** of the application — the code that actually does the work of managing contacts.

## Files

| File | What It Does |
|---|---|
| `ContactService.cs` | The service that adds, finds, and removes contacts from the address book |

## What the Contact Service Can Do

| Action | What Happens |
|---|---|
| **Get all contacts** | Returns a list of every contact currently stored |
| **Get one contact** | Finds a specific contact by their ID number |
| **Add a contact** | Saves a new contact and automatically gives it an ID number |
| **Delete a contact** | Removes a contact by their ID number |

## Important Design Details

- **Shared address book** — The contact list is shared by everyone using the program. If one person adds a contact, everyone else can see it too.
- **Temporary storage** — Contacts are stored in the computer's memory, not in a database. This means **all contacts disappear when the program is restarted**. Think of it like a whiteboard that gets erased when you close the program.
- **No database** — There's no permanent storage like SQL or a file on disk. Everything lives in memory only.