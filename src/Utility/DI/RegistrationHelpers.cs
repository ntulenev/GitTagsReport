using System.Diagnostics;

using LibGit2Sharp;

using Microsoft.Extensions.DependencyInjection;

using Abstractions;
using Models;
using Utility.Console;
using Logic;

namespace Utility.DI;

/// <summary>
/// Provides extension methods for registering application services and logic components.
/// </summary>
public static class RegistrationHelpers
{
    /// <summary>
    /// Registers the main application service.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    public static void RegisterApp(this IServiceCollection services) => services.AddSingleton<Application>();

    /// <summary>
    /// Registers the core logic components required for Git reporting as singleton services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    public static void RegisterLogic(this IServiceCollection services)
    {
        services.AddSingleton<IGitReportBuilder, GitReportBuilder>();
        services.AddSingleton<IGitTagsLoader, GitTagsLoader>();
        services.AddSingleton<IGitReportPrinter, GitReportPrinter>();
        services.AddSingleton<Func<GitPath, IRepository>>(p =>
        {
            Debug.Assert(p is not null);
            return new Repository(p.Value);
        });
    }
}