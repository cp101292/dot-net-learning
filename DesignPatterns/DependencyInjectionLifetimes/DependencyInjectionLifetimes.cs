// ---------------------------------------------------------------------------
// Problem:
// Explain the difference between Singleton, Scoped, and Transient lifetimes
// in ASP.NET Core Dependency Injection. Cover the typical EF Core DbContext
// lifetime and why, what happens when a Singleton directly depends on a
// Scoped service, and a production bug or performance problem caused by
// choosing the wrong lifetime.
// ---------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;

namespace DesignPatterns.DependencyInjectionLifetimes;

public static class DependencyInjectionLifetimesDemo
{
    public static void Run()
    {
        var services = new ServiceCollection();
        services.AddSingleton<SingletonProbe>();
        services.AddScoped<ScopedProbe>();
        services.AddTransient<TransientProbe>();

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });

        Console.WriteLine("Lifetime identity across two scopes:");
        PrintScope(provider, "Scope A");
        PrintScope(provider, "Scope B");

        Console.WriteLine();
        Console.WriteLine("Singleton depending directly on Scoped:");
        DemonstrateInvalidDependency();
    }

    private static void PrintScope(ServiceProvider provider, string scopeName)
    {
        using var scope = provider.CreateScope();
        var scopedProvider = scope.ServiceProvider;
        var firstTransient = scopedProvider.GetRequiredService<TransientProbe>();
        var secondTransient = scopedProvider.GetRequiredService<TransientProbe>();

        Console.WriteLine($"{scopeName}: singleton={provider.GetRequiredService<SingletonProbe>().Id}");
        Console.WriteLine($"{scopeName}: scoped={scopedProvider.GetRequiredService<ScopedProbe>().Id}");
        Console.WriteLine($"{scopeName}: transient={firstTransient.Id}, {secondTransient.Id}");
    }

    private static void DemonstrateInvalidDependency()
    {
        var services = new ServiceCollection();
        services.AddScoped<RequestContext>();
        services.AddSingleton<SingletonWorker>();

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true
        });

        try
        {
            provider.GetRequiredService<SingletonWorker>();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    private abstract class Probe
    {
        protected Probe() => Id = Guid.NewGuid().ToString("N")[..8];

        public string Id { get; }
    }

    private sealed class SingletonProbe : Probe;

    private sealed class ScopedProbe : Probe;

    private sealed class TransientProbe : Probe;

    private sealed class RequestContext;

    private sealed class SingletonWorker(RequestContext requestContext)
    {
        private RequestContext RequestContext { get; } = requestContext;
    }
}