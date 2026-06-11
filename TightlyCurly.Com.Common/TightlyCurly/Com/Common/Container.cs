using System;

namespace TightlyCurly.Com.Common;

/// <summary>
/// Legacy static service locator, formerly backed by Castle Windsor. Now a thin
/// facade over the application's Microsoft.Extensions.DependencyInjection container
/// (see Container.Initialize in Program.cs) kept for the remaining static callers
/// (e.g. the User model). New code should use constructor injection instead.
/// </summary>
public static class Container
{
    private static IServiceProvider _serviceProvider;

    public static void Initialize(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public static object Resolve(Type type)
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("Container has not been initialized. Call Container.Initialize(IServiceProvider) at application startup.");
        }
        object service = _serviceProvider.GetService(type);
        if (service == null)
        {
            throw new InvalidOperationException($"No service for type '{type}' has been registered.");
        }
        return service;
    }

    public static T Resolve<T>()
    {
        return (T)Resolve(typeof(T));
    }
}
