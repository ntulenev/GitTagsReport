using CommandLine;

using Abstractions;
using Models;

namespace Utility.Console;

/// <summary>
/// Represents the core application class that orchestrates the parsing of command-line arguments
/// and the execution of the application logic based on those arguments.
/// </summary>
public sealed class Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Application"/> class with the necessary dependencies.
    /// </summary>
    /// <param name="reportBuilder">The report builder used to generate git reports.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="reportBuilder"/> is null.</exception>
    public Application(IGitReportBuilder reportBuilder)
    {
        ArgumentNullException.ThrowIfNull(reportBuilder);
        _reportBuilder = reportBuilder;
    }

    /// <summary>
    /// Parses the command-line arguments and executes the application logic based on the provided options.
    /// </summary>
    /// <param name="args">The command-line arguments passed to the application.</param>
    public void Run(params string[] args)
    {
        _ = Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                try
                {
                    var gitPath = new GitPath(options.DirectoryPath);
                    var taskTag = new TicketKey(options.TicketKey);

                    _reportBuilder.Build(gitPath, taskTag);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An error occurred: {ex.Message}");
                }
            })
            .WithNotParsed(_ => System.Console.WriteLine("Invalid command-line arguments."));
    }

    private readonly IGitReportBuilder _reportBuilder;
}