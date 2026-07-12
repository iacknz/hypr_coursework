# Properties

This folder contains **startup settings** — instructions that tell the program how to behave when it first turns on.

## Files

| File | What It Does |
|---|---|
| `launchSettings.json` | Contains profiles for different ways to run the program (e.g., development vs. production) |

## What These Settings Control

- **Which port to use** — Decides the web address and port number the program listens on (e.g., `http://localhost:5000`)
- **Environment mode** — Tells the program whether it's running in "development" (for building and testing) or "production" (for real users)
- **Environment variables** — Extra settings that can change how the program behaves without modifying the code

Think of this folder as the **control panel** for how the program starts up.