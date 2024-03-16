using Models;

namespace Abstractions;

/// <summary>
/// Defines the contract for a service that prints reports based on Git tag metadata and a specified task filter.
/// </summary>
public interface IGitReportPrinter
{
    /// <summary>
    /// Prints a report for the specified Git tag metadata items, filtered by a ticket key.
    /// </summary>
    /// <param name="items">The collection of Git tag metadata to include in the report.</param>
    /// <param name="taskFilter">The ticket key to filter the report by. Only description related to this ticket key
    /// should be included.</param>
    public void Print(IEnumerable<GitTagMetadata> items, TicketKey taskFilter);
}
