using CommandLine;

namespace Utility.Console;

/// <summary>
/// Represents the command-line options for the application.
/// </summary>
/// <remarks>
/// This class defines the required options that the user must provide when running the application
/// from the command line.
/// Each option is decorated with attributes that specify how the option is parsed and what help
/// text is displayed to the user.
/// </remarks>
public sealed class Options
{
    /// <summary>
    /// Gets the path to the git directory.
    /// </summary>
    /// <value>
    /// The path specifies the location of the git repository that the application will operate on.
    /// This option is required and is identified by the '-p' short name or '--path' long name on the command line.
    /// </value>
    [Option('p', "path", Required = true, HelpText = "Path to git directory.")]
    public required string DirectoryPath { get; init; }

    /// <summary>
    /// Gets the ticket system task key.
    /// </summary>
    /// <value>
    /// The key used to identify tasks in the ticketing system.
    /// This option is required and is identified by the '-k' short name or '--key' long name on the command line.
    /// </value>
    [Option('k', "key", Required = true, HelpText = "Ticket system Task key.")]
    public required string TicketKey { get; init; }
}