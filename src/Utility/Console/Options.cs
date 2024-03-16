using CommandLine;

namespace Utility.Console;

public sealed class Options
{
    [Option('p', "path", Required = true, HelpText = "Path to git directory.")]
    public required string DirectoryPath { get; init; }

    [Option('k', "key", Required = true, HelpText = "Ticket system Task key.")]
    public required string TicketKey { get; init; }
}