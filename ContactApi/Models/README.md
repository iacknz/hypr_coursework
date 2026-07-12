# Models

This folder defines what a **contact** looks like — think of it as the template or blueprint for a contact card.

## Files

| File | What It Does |
|---|---|
| `Contact.cs` | Defines the shape of a contact — what information we store and what rules it must follow |

## What Information We Store About Each Contact

| Piece of Info | Type of Data | Rules |
|---|---|---|
| ID | Number | Automatically assigned by the system |
| First Name | Text | Required, maximum 20 characters |
| Last Name | Text | Required, maximum 20 characters |
| Address | Text | Required, maximum 50 characters |
| Phone Number | Text | Required, must be exactly 11 digits (e.g., 04123456789) |
| Age | Number | Required, must be between 0 and 999 |
| Post Code | Text | Required, must be exactly 4 digits (e.g., 2000) |

## How Validation Works

Before saving a contact, the system checks that all the rules above are followed. For example:
- If someone tries to save a name that's too long, the system will reject it
- If a phone number has letters in it, the system will reject it
- If any required field is missing, the system will reject it

This ensures the data stays clean and reliable.