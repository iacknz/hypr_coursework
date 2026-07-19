# Work Item 001 — Contact UI Screen

> As an **end user**, I want a **web-based contact management screen**, so that **I can view, add, and delete contacts through a graphical interface instead of raw API calls**.

## Notes

Build a single-page HTML/JavaScript UI that connects to the Contact API (`/contacts` endpoints). The form captures all Contact model fields (FirstName, LastName, Address, PhoneNumber, Age, PostCode). **LastName and Address are mandatory** — the UI must enforce these client-side before submitting. The screen shows a list of existing contacts with the ability to add new ones and delete existing ones.

**Map Integration**: After a contact is successfully submitted to the API (POST returns 201), the UI should geocode the contact's address using a free service (e.g., OpenStreetMap Nominatim API) and display an interactive map (e.g., Leaflet or OpenStreetMap embed) showing a pin at that location. The map should also be visible when viewing a contact in the list — clicking or expanding a contact row reveals the map for that contact's address.

### Acceptance Criteria (Given-When-Then Format)

#### Task 1: Display contact list on page load
- **Given**: The Contact API is running with some contacts in the in-memory store
- **When**: The user opens the UI page
- **Then**: A list/table of all contacts is displayed, showing FirstName, LastName, Address, PhoneNumber, Age, and PostCode for each
- **Status**: ❌ Not Started

#### Task 2: Add a new contact via the form
- **Given**: The user has filled in all fields on the contact form
- **When**: The user clicks "Add Contact"
- **Then**: A POST request is sent to `/contacts`, the new contact appears in the list, and the form resets
- **Status**: ❌ Not Started

#### Task 3: Enforce LastName and Address as mandatory (client-side)
- **Given**: The user tries to submit the form with an empty LastName or Address field
- **When**: The user clicks "Add Contact"
- **Then**: The submission is blocked and clear validation messages are shown for the empty mandatory fields
- **Status**: ❌ Not Started

#### Task 4: Handle API validation errors gracefully
- **Given**: The user submits the form with invalid data (e.g., non-numeric age, wrong phone format)
- **When**: The API returns a 400 validation error response
- **Then**: The UI displays the server-side error messages to the user without losing their input
- **Status**: ❌ Not Started

#### Task 5: Delete a contact from the list
- **Given**: The contact list is displayed with at least one contact
- **When**: The user clicks a "Delete" button next to a contact
- **Then**: A DELETE request is sent to `/contacts/{id}`, the contact is removed from the list, and a success indicator is shown
- **Status**: ❌ Not Started

#### Task 6: Show interactive map after successful contact submission
- **Given**: The user has filled in all fields and submitted the form
- **When**: The API returns a 201 Created response for the new contact
- **Then**: The address is geocoded via a free service (e.g., OpenStreetMap Nominatim), and an interactive map (Leaflet) is displayed below the form with a pin at the resolved location
- **Status**: ❌ Not Started

#### Task 7: Show map for existing contacts in the list
- **Given**: The contact list is displayed with contacts that have addresses
- **When**: The user clicks or expands a contact row
- **Then**: An interactive map is revealed showing a pin at that contact's address location
- **Status**: ❌ Not Started

#### Task 8: Full keyboard navigation (tabbing)
- **Given**: The UI is loaded in a browser
- **When**: The user presses Tab to navigate through all interactive elements
- **Then**: Every form field, button, and contact row is reachable via keyboard, with a visible focus indicator on each element
- **Status**: ❌ Not Started

#### Task 9: Colour contrast meets WCAG AA standards
- **Given**: The UI is rendered in a browser
- **When**: Colour contrast is measured for all text against its background
- **Then**: All text meets at least WCAG AA contrast ratios (4.5:1 for normal text, 3:1 for large text), and error/validation messages use colour plus an icon or text label (not colour alone)
- **Status**: ❌ Not Started

#### Task 10: Screen reader support
- **Given**: A screen reader user opens the UI
- **When**: Navigating through the page
- **Then**: All form inputs have associated `<label>` elements, buttons have descriptive text, status messages (success, error, map loading) are announced via `aria-live` regions, and the contact list uses proper semantic table markup or `role="grid"` with accessible row actions

## Current Task Focus

- **Active Task**: Task 1: Display contact list on page load
- **Stage**: PLAN
- **Branch**: `feature/contact-ui-screen`
- **Last Updated**: 2026-07-19

### STAGE 1: PLAN
- **Test Strategy**: What tests are needed for confidence?
  - [ ] Manual test: Open the UI and verify contacts appear in the list
  - [ ] Manual test: Verify empty state shows a "No contacts" message
  - [ ] Edge case: API is unreachable — show an error state
  - [ ] Manual test: Submit a contact with a valid address and verify a map appears with a pin at the correct location
  - [ ] Manual test: Click a contact row and verify the map renders for that address
  - [ ] Edge case: Geocoding fails (invalid address) — show a friendly message instead of a map
  - [ ] Accessibility: Tab through all interactive elements — verify every field, button, and row is reachable with visible focus indicator
  - [ ] Accessibility: Run a colour contrast checker (e.g., browser DevTools or axe DevTools) — verify all text meets WCAG AA ratios and errors use colour + icon/text
  - [ ] Accessibility: Test with a screen reader (e.g., Windows Narrator or NVDA) — verify labels, ARIA live regions, and semantic markup are announced correctly
- **File Changes**: What code changes are needed?
  - [ ] `ui/index.html` — Main HTML structure with form, contact list, and map containers; include Leaflet CSS/JS from CDN
  - [ ] `ui/style.css` — Styling for the contact management screen and map containers
  - [ ] `ui/app.js` — JavaScript logic for API calls, form handling, rendering, geocoding via Nominatim API, and Leaflet map rendering
  - [ ] `ui/README.md` — Documentation for the UI project
- **Planning Status**: 🔄 In Progress

### STAGE 2: BUILD & ASSESS
- **Implementation Progress**:
  - [ ] Tests written and initially failing
  - [ ] Core functionality implemented
  - [ ] Tests passing
  - [ ] Edge cases handled
- **Quality Validation**:
  - [ ] UI renders correctly in a modern browser
  - [ ] Form validation enforces LastName and Address as required
  - [ ] All CRUD operations work against the running API
  - [ ] Error states handled gracefully
  - [ ] Keyboard navigation: all elements reachable via Tab with visible focus indicators
  - [ ] Colour contrast: WCAG AA minimum ratios met (4.5:1 normal text, 3:1 large text)
  - [ ] Screen reader: labels, ARIA live regions, and semantic structure verified
- **Build & Assess Status**: ❌ Not Started

### STAGE 3: REFLECT & ADAPT
- **Process Assessment**:
  - What went well in this iteration?
  - What friction was encountered?
  - How can the process be improved?
- **Future Task Assessment**:
  - Do remaining tasks need adjustment based on this implementation?
  - Are new tasks needed?
  - Should task order be rearranged?
- **Template/Process Updates**: [Any changes to make to templates or workflow]
- **Reflect & Adapt Status**: ❌ Not Started

### STAGE 4: COMMIT & PICK NEXT
- **Commit Details**:
  - **Message**: `feat(ui): add contact management screen with CRUD operations`
  - **Branch**: `feature/contact-ui-screen`
  - **Files Changed**: `ui/index.html`, `ui/style.css`, `ui/app.js`, `ui/README.md`
- **README Review**: Check README files in every folder touched or created. Update any that don't accurately reflect what's there now.
  - [ ] READMEs reviewed and updated as needed
- **Purge**: Delete stages 1–3 details above from this work item after committing. Keep only acceptance criteria and status.
- **Next Task Selection**: Task 2: Add a new contact via the form
- **Commit & Pick Next Status**: ❌ Not Started

---

## Instructions for Use

1. **Story Setup**: When starting this work item, fill in all story details and acceptance criteria
2. **Task Iteration**: For each task, update the "Current Task Focus" section with detailed tracking
3. **Task Completion**: When a task is completed and committed:
   - Mark the task as ✅ Complete in the acceptance criteria
   - **Purge all stage details** (PLAN, BUILD & ASSESS, REFLECT & ADAPT, COMMIT & PICK NEXT notes) from "Current Task Focus"
   - Update "Current Task Focus" with the next task details
   - This keeps the document current and focused — working scaffolding is noise after the commit

### Quality Checklist
- [ ] Acceptance criteria is in Given-When-Then format
- [ ] Each task maps to roughly 1-3 commits
- [ ] File changes list concrete files with descriptions
- [ ] Test strategy covers happy path, errors, and edge cases
- [ ] Branch name follows convention: `feature/description`
- [ ] Map integration uses a free geocoding service (no API keys required)
- [ ] Accessibility tests for tabbing, colour contrast, and screen reader included
