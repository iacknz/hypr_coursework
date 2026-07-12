# User Guide — Feature Flags & API Versioning

> **For non-technical users.** No coding knowledge needed.

---

## What Are Feature Flags?

**Feature flags are like light switches for different parts of the program.**

Imagine you've built a new feature — like the ability to edit a contact after it's been created. You're not sure if it's ready for everyone to use yet. Instead of deleting the code and writing it again later, you just **flip a switch** to hide it until you're ready.

### The Light Switch Analogy

```
                    ┌─────────────────────┐
                    │  appsettings.json    │
                    │  (the switchboard)   │
                    ├─────────────────────┤
                    │  EnableUpdateEndpoint│  ←── Turn this to "true"
                    │  = false             │      to turn on editing
                    └─────────────────────┘
```

- **Switch OFF (`false`)** — The feature is hidden. If someone tries to use it, they'll get a "not found" message as if it doesn't exist.
- **Switch ON (`true`)** — The feature is available for everyone to use.

### Why Use Feature Flags?

| Benefit | Explanation |
|---------|-------------|
| **🚀 Ship early, turn on later** | You can add code to the program without making it visible. Deploy first, flip the switch when ready. |
| **🛑 Instant rollback** | If a new feature causes problems, just flip the switch OFF. No need to undo all the code changes. |
| **🧪 Test in real life** | Turn a feature on for a small group first, see how it behaves, then turn it on for everyone. |
| **🔒 Hide unfinished work** | No more keeping unfinished features on a separate branch that goes out of date. |

### Current Feature Flags

| Flag Name | What It Controls | Default |
|-----------|-----------------|---------|
| `EnableUpdateEndpoint` | Turns on the ability to **edit** an existing contact | OFF |
| `EnableSearchEndpoint` | Turns on the ability to **search** contacts by name | OFF |
| `EnableV2Api` | Turns on the **v2 API** (newer version of the program) | OFF |

### How to Turn a Feature On

1. Open the file **`appsettings.json`** (in the `ContactApi` folder)
2. Find the section that says `"FeatureManagement"`
3. Change `false` to `true` for the feature you want to turn on
4. Save the file and restart the program

**Example — turning on contact editing:**

```json
"FeatureManagement": {
    "EnableUpdateEndpoint": true,    ← changed from false to true
    "EnableSearchEndpoint": false,
    "EnableV2Api": false
}
```

That's it! The feature will now be available.

---

## What Is API Versioning?

**API versioning is like having different editions of the same product.**

Think of it like a smartphone. The iPhone 14 and iPhone 15 both make calls and take photos, but the iPhone 15 has newer features. People with an iPhone 14 don't suddenly stop working — they just don't get the new stuff.

### The Edition Analogy

```
/v1/contacts  ────  Like the "Standard Edition"
                     Stable, reliable, won't change

/v2/contacts  ────  Like the "Deluxe Edition"
                     Has everything v1 has, plus new features
```

### Why Use Versioning?

| Benefit | Explanation |
|---------|-------------|
| **🔄 No breaking changes** | If someone built a tool that uses your API, updating the API won't break their tool. They can keep using the old version. |
| **⏳ Gradual upgrades** | Users can move to the new version when they're ready, not when you force them. |
| **🧹 Clean evolution** | You can improve the program without being afraid of breaking things. Old and new versions run side by side. |

### What's Different Between Versions

| Feature | Legacy `/contacts` | v1 `/v1/contacts` | v2 `/v2/contacts` |
|---------|-------------------|-------------------|-------------------|
| List contacts | ✅ | ✅ | ✅ |
| Get one contact | ✅ | ✅ | ✅ |
| Add a contact | ✅ | ✅ | ✅ |
| Delete a contact | ✅ | ✅ | ✅ |
| Edit a contact | ❌ | 🔸 (behind flag) | ✅ |
| Search contacts | ❌ | ❌ | 🔸 (behind flag) |

- ✅ = Always available
- 🔸 = Needs feature flag turned on
- ❌ = Not available

### How to Use a Different Version

The version is in the web address (URL). Just change the number:

| To use... | Call this address |
|-----------|------------------|
| Legacy (original) | `http://localhost:5000/contacts` |
| v1 (stable) | `http://localhost:5000/v1/contacts` |
| v2 (enhanced) | `http://localhost:5000/v2/contacts` |

---

## Putting It All Together — Common Scenarios

### Scenario 1: "I want to test the edit feature before releasing it"

1. Open `appsettings.json`
2. Set `"EnableUpdateEndpoint": true`
3. Restart the program
4. Test editing at `PUT /v1/contacts/{id}`
5. When you're happy, leave it on. If something breaks, set it back to `false`.

### Scenario 2: "I want to try the new v2 API"

1. Open `appsettings.json`
2. Set `"EnableV2Api": true`
3. Optionally also set `"EnableSearchEndpoint": true` for search
4. Restart the program
5. Try the v2 endpoints at `http://localhost:5000/v2/contacts`

### Scenario 3: "The new search feature is causing errors, turn it off!"

1. Open `appsettings.json`
2. Set `"EnableSearchEndpoint": false`
3. Restart the program
4. The search endpoint is now hidden. Everything else still works perfectly.

### Scenario 4: "I have an old app that uses `/contacts` — will it break?"

No! The legacy `/contacts` routes still work exactly as before. Your old app doesn't need to change anything.

---

## Quick Reference

| File | What It Does |
|------|-------------|
| `appsettings.json` | The switchboard — turn features on/off here |
| `FeatureFlags.cs` | The list of all available switches (for developers) |
| `Program.cs` | The main program that checks the switches (for developers) |

**Remember:**
- Feature flags = light switches for features
- Versioning = different editions of the same API
- Change `false` to `true` in `appsettings.json` to turn something on
- Old versions never break when new versions are added