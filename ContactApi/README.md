# ContactApi

This is the main folder for the **Contact API** project — a small computer program that stores and manages contact information (like names, phone numbers, and addresses).

Think of it like a digital address book that other programs can talk to over the internet.

## What's Inside

| Folder/File | What It Does |
|---|---|
| [`Models/`](Models/README.md) | Defines what a "contact" looks like — what pieces of information we store about each person |
| [`Services/`](Services/README.md) | The brain of the operation — handles adding, finding, and deleting contacts |
| [`Properties/`](Properties/README.md) | Settings that control how the program starts up and runs |
| [`context/`](context/README.md) | Project knowledge base — explains what this project is, how it works, and what tools it uses |
| `Program.cs` | The main instruction file that ties everything together and defines what the API can do |
| `ContactApi.csproj` | A recipe file that lists what ingredients (libraries) the project needs |
| `appsettings.json` | Configuration settings like environment mode and logging preferences |
| `ContactApi.http` | A test file you can use to try out the API endpoints |

## How It's Built

- **Language:** Written in C# (pronounced "C sharp"), a programming language by Microsoft
- **Framework:** Built on ASP.NET Core, a Microsoft toolkit for creating web programs
- **Validation:** Uses a tool called MiniValidation to check that contact info is correct before saving it
- **Documentation:** Automatically generates API docs when running in development mode