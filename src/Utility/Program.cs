using Microsoft.Extensions.DependencyInjection;

using Utility.Console;
using Utility.DI;

var serviceCollection = new ServiceCollection();
serviceCollection.RegisterLogic();
serviceCollection.RegisterApp();
var provider = serviceCollection.BuildServiceProvider();
using var serviceScope = provider.CreateScope();
var scopeProvider = serviceScope.ServiceProvider;

#pragma warning disable CA1031 // Do not catch general exception types
try
{
    var app = scopeProvider.GetRequiredService<Application>();
    app.Run(args);
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
}
#pragma warning restore CA1031 // Do not catch general exception types