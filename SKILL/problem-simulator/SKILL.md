---
name: problem-simulator
description: 'Turn a described C#/.NET scenario into a runnable, well-documented simulation in this repo: capture the question as a code comment, implement a clean solution, wire it into Program.cs so it runs from the root Main, add a senior-level concept README, and build+run to verify before signoff. Use when the user describes a coding problem, interview question, bug scenario, or concept they want demonstrated/simulated in a .NET/C# workspace.'
argument-hint: 'Describe the scenario/problem/concept to simulate'
---

# Problem Simulator

Converts a described scenario into an organized, runnable C# example with documentation, inside a .NET solution/project.

## When to Use
- User describes a coding problem, interview question, bug, or design scenario and wants it simulated in code.
- User wants to learn a C#/.NET concept via a concrete, runnable example rather than an explanation only.
- Any request to "simulate", "demonstrate", or "create an example for" a scenario in this workspace.

## Procedure (follow in order)

1. **Understand the scenario** — restate the problem in your own words; ask only if genuinely ambiguous (see [workflow](./references/workflow.md#1-understand-the-scenario)).
2. **Pick the folder** — group by high-level concept area, then a specific problem folder. Never dump into one flat folder. See [folder conventions](./references/folder-conventions.md).
3. **Document the question** — as a properly formatted comment at the top of the main C# file. See [documentation standards](./references/documentation-standards.md#1-the-question-comment).
4. **Implement the simulation** — clear, readable code with precise (not verbose) what+why comments. See [documentation standards](./references/documentation-standards.md#2-code-comments).
5. **Wire into root `Main`** — expose a single static entry method and call it from `Program.cs`. See [workflow](./references/workflow.md#5-wire-into-program-cs).
6. **Write the concept README** — senior-developer-level notes (motive, why this approach, tradeoffs, in-depth points) for the problem folder. See [documentation standards](./references/documentation-standards.md#3-concept-readme).
7. **Build and run** — verify actual output matches expected behavior before telling the user it's done. See [workflow](./references/workflow.md#7-build-and-verify).

Full step-by-step detail (including what "done" looks like for each step) lives in [references/workflow.md](./references/workflow.md).

## Quick Reference
| Concern | File |
|---|---|
| Detailed step-by-step workflow | [references/workflow.md](./references/workflow.md) |
| Folder/naming conventions | [references/folder-conventions.md](./references/folder-conventions.md) |
| Comment & README writing standards | [references/documentation-standards.md](./references/documentation-standards.md) |
