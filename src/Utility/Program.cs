using Microsoft.Extensions.DependencyInjection;

using Utility.Console;
using Utility.DI;

var serviceCollection = new ServiceCollection();
serviceCollection.RegisterLogic();
serviceCollection.RegisterApp();
var provider = serviceCollection.BuildServiceProvider();
using var serviceScope = provider.CreateScope();
var scopeProvider = serviceScope.ServiceProvider;

try
{
    var app = scopeProvider.GetRequiredService<Application>();
    app.Run(args);
}
catch (Exception ex)
{
    Console.WriteLine($"Application error: {ex.Message}");
}