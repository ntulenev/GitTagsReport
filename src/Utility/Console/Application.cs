using Abstractions;
using CommandLine;
using Models;

namespace Utility.Console;

public sealed class Application
{
    public Application(IGitReportBuilder reportBuilder)
    {
        ArgumentNullException.ThrowIfNull(reportBuilder);
        _reportBuilder = reportBuilder;
    }
    public void Run(params string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                try
                {
                    var gitPath = new GitPath(options.DirectoryPath);
                    var taskTag = new TicketKey(options.TicketKey);
                    
                    _reportBuilder.Build(gitPath,taskTag);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"An error occurred: {ex.Message}");
                }
            })
            .WithNotParsed(errors => { System.Console.WriteLine("Invalid command-line arguments."); });
    }

    private readonly IGitReportBuilder _reportBuilder;
}