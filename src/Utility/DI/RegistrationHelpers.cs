using Abstractions;
using Logic;
using Microsoft.Extensions.DependencyInjection;
using Utility.Console;

namespace Utility.DI;

public static class RegistrationHelpers
{
    public static void RegisterApp(this IServiceCollection services)
    {
        services.AddSingleton<Application>();
    }

    public static void RegisterReports(this IServiceCollection services)
    {
        services.AddSingleton<IGitReportBuilder, GitReportBuilder>();
        services.AddSingleton<IGitTagsLoader, GitTagsLoader>();
        services.AddSingleton<IGitReportPrinter, GitReportPrinter>();
    }
}