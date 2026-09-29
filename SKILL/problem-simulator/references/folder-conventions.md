# Folder Conventions

Goal: keep the repo browsable — grouped by concept area, not one folder per problem scattered at root, and not a single giant flat "Problems" dump.

## Two-Level Structure

```
<ProjectRoot>/
  <CategoryFolder>/            # high-level concept area (parent)
    <ProblemFolder>/            # one specific scenario
      <ProblemName>.cs          # question comment + implementation
      README.md                 # concept deep-dive for this problem
    <AnotherProblemFolder>/
      ...
  Program.cs                    # calls into problem folders
```

## Choosing the Category Folder

Pick (or reuse) a category based on the dominant .NET/CS concept, not the surface story of the problem. Examples:

- `Concurrency` — threading, locks, deadlocks, `async`/`await`, race conditions, `Task` pitfalls.
- `Collections` — `List`, `Dictionary`, `IEnumerable`/`IEnumerator`, `yield`, LINQ deferred execution.
- `MemoryAndGC` — boxing, value vs reference types, `IDisposable`, finalizers, memory leaks.
- `DesignPatterns` — Singleton, Factory, Observer, Strategy, etc.
- `ErrorHandling` — exception filters, custom exceptions, retry policies.
- `TypeSystemAndOOP` — generics, variance, interfaces vs abstract classes, polymorphism edge cases.
- `Delegates_Events_Functional` — delegates, events, `Func`/`Action`, closures.

Before inventing a new category, check existing folders — reuse if the scenario clearly fits. Only create a new category when none of the existing ones reasonably apply.

## Naming the Problem Folder

- PascalCase, short, descriptive of the _specific scenario_ (not the category again): `ProducerConsumerDeadlock`, `ClosureCaptureBug`, `DeferredLinqExecution`.
- The main `.cs` file inside typically matches the folder name (e.g. `ProducerConsumerDeadlock.cs`) or uses a clear `*Demo.cs` suffix if the folder name is also the domain type name.

## Namespaces

- Namespace mirrors the folder path: `namespace Concurrency.ProducerConsumerDeadlock;`
- Keeps `Program.cs` calls unambiguous and IntelliSense-friendly.

## Program.cs Wiring

- `Program.cs` stays at project root and only contains calls into problem folders (plus minimal setup) — no business logic lives there.
- Only the newest demo call is active at a time; every earlier demo call remains present but commented out (not deleted), so any past scenario can be re-enabled by uncommenting it.
