using Models;

namespace Abstractions;

/// <summary>
/// Defines the contract for a service that builds Git reports based on a repository path and a specific ticket key.
/// </summary>
public interface IGitReportBuilder
{
    /// <summary>
    /// Builds and processes a Git report for the specified repository path and ticket key.
    /// </summary>
    /// <param name="path">The path to the Git repository from which the report will be generated.</param>
    /// <param name="ticketKey">The ticket key that the report is focused on.</param>
    public void Build(GitPath path, TicketKey ticketKey);
}
