# problem-simulator (GitHub project skill)

Turns a described C#/.NET scenario into a runnable, documented simulation inside your current workspace.

## What It Produces

For a given scenario, this skill creates:

- A category folder (grouped by high-level concept) containing a problem-specific subfolder.
- A `.cs` file with the original question documented as a top comment, plus a clean, precisely-commented implementation.
- A `README.md` in the problem folder explaining the concepts at a senior-developer level (motive, mechanics, tradeoffs, gotchas).
- A wired-up call from the root `Program.cs` so the demo runs immediately via `dotnet run`.
- A verified build + run before signoff.

## Example Prompts

- "Simulate a deadlock caused by two locks acquired in different order."
- "Show me deferred execution in LINQ with a bug where the source collection changes before enumeration."
- "Demonstrate why mutable structs in collections are a footgun."
- "Simulate a race condition on a shared counter without locking, then fix it."

## Files

- [SKILL.md](./SKILL.md) — entry point, procedure summary, links to references.
- [references/workflow.md](./references/workflow.md) — full 7-step process with "done" criteria.
- [references/folder-conventions.md](./references/folder-conventions.md) — category/problem folder naming and structure.
- [references/documentation-standards.md](./references/documentation-standards.md) — question-comment format, code comment style, README template.

## Notes

- This project skill is stored in `.github/skills/problem-simulator/` and is available to agents working in this repository.
- The skill adapts the category list in `folder-conventions.md` to whatever categories already exist in the target repo — it reuses folders rather than creating near-duplicates.
