# Dependency Injection Lifetimes

## What This Demonstrates

The built-in ASP.NET Core container keeps a singleton for the provider's lifetime, a scoped service once per scope, and a transient service once per resolution. The run also demonstrates the container rejecting a singleton that directly captures a scoped dependency when scope validation is enabled.

## Why This Approach

Lifetimes define ownership and reuse, not merely object-creation cost. A scope is a unit of work (usually one HTTP request); its services can safely hold request-specific state and are disposed with that scope. Singletons are shared across concurrent requests, so they need thread-safe behavior and must not retain request-specific dependencies.

The sample uses the same `Microsoft.Extensions.DependencyInjection` container used by ASP.NET Core, with scope validation enabled. The IDs make reuse boundaries observable without needing a web server or a database.

## Key Concepts

- **Singleton:** one instance per root service provider; appropriate for immutable or thread-safe shared services.
- **Scoped:** one instance per scope; ASP.NET Core creates a scope for each request.
- **Transient:** a new instance per resolution; useful for lightweight, stateless services.
- **Scope validation:** catches scoped services resolved from the root provider and scoped dependencies captured by singletons.
- **EF Core `DbContext`:** typically scoped via `AddDbContext`, giving each request its own unit of work, change tracker, and disposal boundary.

## EF Core `DbContext` Lifetime

`AddDbContext<TContext>` registers the context as scoped by default. A context is designed for a short unit of work and is not thread-safe; sharing it across requests risks concurrent operations, cross-request tracked state, and delayed resource cleanup. For background jobs or workflows needing multiple units of work, create a scope per operation or use `IDbContextFactory<TContext>` rather than injecting a context into a singleton.

## Tradeoffs & Alternatives

## Tradeoffs & Alternatives

| Lifetime / Alternative | Benefits | Tradeoffs / When to Use |
|---|---|---|
| **Singleton** | Minimizes repeated construction and provides one shared instance. | Introduces shared-state and concurrency obligations. Use for immutable or thread-safe services. |
| **Transient** | Avoids shared instance state by creating a new instance for each resolution. | Can cause substantial allocation or resource churn when the service owns expensive work. |
| **Scoped** | Aligns naturally with request-specific state and disposal boundaries. | Code running outside HTTP requests must create and dispose scopes explicitly. |
| **`IDbContextFactory<TContext>`** | Allows context creation independently of an ambient request scope and supports creating multiple contexts within one scope. | Adds factory-based creation instead of relying directly on the scoped `DbContext` instance. Use when independent or multiple context instances are required. |

## Gotchas / Common Mistakes

- A singleton directly depending on a scoped service is a captive dependency. With scope validation enabled, resolution throws `InvalidOperationException`.
- With validation disabled, resolving a scoped dependency from the root provider can make it live as long as the root provider. A singleton that captures it can then reuse request-bound state across operations and delay disposal.
- A singleton background worker should inject `IServiceScopeFactory`, create a scope for each job, and resolve scoped dependencies from that scope.
- Scoped does not mean thread-safe: parallel work inside one request must not concurrently use the same `DbContext`.
- Transient does not guarantee immediate disposal; container-created disposable services are disposed with their owning scope/provider.

## Production Scenario

A multi-tenant API has a singleton report scheduler that directly captures a scoped `TenantContext` or `DbContext`. In development, scope validation exposes the invalid graph. If production disables validation and the dependency is resolved from the root provider, the service may retain the first tenant's context or a shared EF change tracker. Reports can then use stale or wrong-tenant state, concurrent jobs can collide on a non-thread-safe context, and tracked entities/resources can accumulate. The fix is to inject `IServiceScopeFactory`, create a scope per job, and resolve the tenant context and `DbContext` inside it.

## How to Run

Run `dotnet run` from the project directory. `Program.cs` invokes `DependencyInjectionLifetimesDemo.Run()`. The output should show one singleton ID across both scopes, one scoped ID per scope, distinct transient IDs for each resolution, and an `InvalidOperationException` message for the invalid singleton-to-scoped dependency.
