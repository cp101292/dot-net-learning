# Workflow: Problem Simulator

Detailed procedure. Each step lists what to do and the "done" criterion before moving on.

## 1. Understand the Scenario

- Restate the scenario back in one or two sentences (problem, inputs/outputs, constraints, expected behavior).
- If the scenario is a well-known concept (e.g., "simulate a race condition", "deadlock example", "demonstrate `IEnumerable` vs `IEnumerator`"), identify the canonical C#/.NET mechanism involved.
- Only ask a clarifying question if the scenario is genuinely ambiguous (e.g., multiple valid interpretations that lead to materially different code). Otherwise, proceed with the most reasonable interpretation.
- **Done when**: you can state, in a sentence, what the code will prove/demonstrate and what "correct output" looks like.

## 2. Pick the Folder

Apply [folder-conventions.md](./folder-conventions.md) to decide:

- The **parent/category folder** (high-level concept area, e.g. `Concurrency`, `Collections`, `DesignPatterns`).
- The **problem folder** name (short, PascalCase, describes the specific scenario, e.g. `ProducerConsumerDeadlock`).
- Reuse an existing category folder if one already fits — do not create near-duplicate categories.
- **Done when**: you have a concrete relative path, e.g. `Concurrency/ProducerConsumerDeadlock/`.

## 3. Document the Question

- At the top of the primary `.cs` file in the problem folder, add a comment block that states the original scenario/question verbatim (or a faithful paraphrase if the user's wording was very long).
- Use the format in [documentation-standards.md](./documentation-standards.md#1-the-question-comment).
- **Done when**: someone with zero context could read the top comment and know exactly what problem the file solves.

## 4. Implement the Simulation

- Write the smallest, clearest program that actually demonstrates the behavior — prefer real demonstration (e.g., actually trigger the race condition, actually show the deferred execution) over merely asserting a claim in comments.
- Follow [documentation-standards.md](./documentation-standards.md#2-code-comments) for comment density and style.
- Keep it runnable standalone (a single static method other code can call) — no unhandled interactive input, no infinite loops without a clear exit.
- **Done when**: the file compiles conceptually and reflects the scenario faithfully; comments explain non-obvious _why_, not restate _what_.

## 5. Wire into Program.cs

- Expose one public static method as the entry point for the scenario, e.g. `public static class ProducerConsumerDeadlockDemo { public static void Run() { ... } }`.
- In the root `Program.cs`, add a single call (top-level statement or inside `Main`) to invoke it, e.g.:
  ```csharp
  Concurrency.ProducerConsumerDeadlock.ProducerConsumerDeadlockDemo.Run();
  ```
- Only the **newest** demo runs by default: comment out (don't delete) any previous demo call already in `Program.cs`, and add the new call active/uncommented. This keeps `dotnet run` fast and output readable — old demos stay available to re-enable by uncommenting.
- **Done when**: running the project executes only this scenario's code path; prior demo calls are present but commented out, not deleted.

## 6. Write the Concept README

- Create `README.md` inside the problem folder (not the parent category folder) covering the concepts used.
- Write it the way a senior engineer would annotate a design doc: motive, why this approach vs. alternatives, in-depth mechanics, tradeoffs/gotchas. See template in [documentation-standards.md](./documentation-standards.md#3-concept-readme).
- **Done when**: the README teaches the concept, not just the code — someone could skip the code and still understand the concept and its tradeoffs.

## 7. Build and Verify

- Build the project/solution and run it.
- Compare actual console output against the expected behavior from step 1.
- If output doesn't match intent (e.g., a race condition didn't manifest, an exception wasn't thrown when expected), fix the code — don't sign off on a simulation that doesn't actually demonstrate the scenario.
- Report build/run success and a short summary of observed output as the sign-off.
- **Done when**: build succeeds, program runs, and output matches the intended demonstration.
