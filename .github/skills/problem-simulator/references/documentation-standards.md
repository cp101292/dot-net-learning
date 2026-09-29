# Documentation Standards

## 1. The Question Comment

Place at the very top of the main `.cs` file, above `namespace`/`using`. Format:

```csharp
// ---------------------------------------------------------------------------
// Problem:
// <verbatim or faithfully paraphrased scenario/question as given by the user>
// ---------------------------------------------------------------------------
```

Rules:

- Preserve the user's intent and constraints exactly; don't silently narrow the scope.
- If the original wording is very long, paraphrase tightly but keep every constraint that affects the implementation.
- Do not add a solution summary here — that belongs in the README (step 6).

## 2. Code Comments

Style: precise, not verbose. One short line explaining _why_, placed only where the code isn't self-explanatory.

- **Do**: comment non-obvious decisions, tricky ordering, intentional bugs/pitfalls being demonstrated, or subtle .NET/CLR behavior.
- **Don't**: restate what the next line obviously does (`// increment i` above `i++`).
- **Don't**: write multi-paragraph doc comments where one line suffices.
- Prefer self-documenting names over comments where possible; comment only the _why_.

Example (good):

```csharp
// Deliberately no lock here — this line is what causes the race condition.
counter++;
```

Example (bad — verbose/obvious):

```csharp
// This method takes an integer and adds one to it and returns the result
// to the caller so that the counter can be incremented for tracking purposes.
public int Increment(int counter) => counter + 1;
```

## 3. Concept README

One `README.md` per problem folder (not per category). Write as a senior engineer's design-review notes: precise, opinionated, no filler. Suggested sections:

Use Markdown tables for structured comparisons, tradeoffs, or other information that is easier to scan in columns. Include concise fenced code snippets when they clarify the demonstrated behavior or show the relevant usage. Use these formats wherever they improve readability and cleanliness; do not force them into sections where prose or lists are clearer.

```markdown
# <Problem Name>

## What This Demonstrates

1-2 sentences: the concept and the specific behavior being shown.

## Why It Happens / Why This Approach

The underlying mechanism (CLR/runtime/language behavior) that causes or enables this.
Explain motive: why the language/runtime works this way, not just that it does.

## Key Concepts

Bullet list of the .NET/C# concepts involved, each with a one-line "why it matters here".

## Tradeoffs & Alternatives

- What this approach costs (performance, complexity, safety) vs. alternative approaches.
- When you'd choose differently in production code.

## Gotchas / Common Mistakes

Real pitfalls engineers hit with this concept, beyond just this toy example.

## How to Run

Reference to the `Program.cs` call / method name that runs this demo.
```

Keep each section to a few lines — this is reference material for someone who already knows C#, not a tutorial for beginners.
