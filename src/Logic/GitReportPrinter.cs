using Abstractions;
using ConsoleTables;
using Models;

namespace Logic;

/// <summary>
/// Prints Git tag reports to the console.
/// </summary>
public sealed class GitReportPrinter : IGitReportPrinter
{
    /// <inheritdoc/>
    public void Print(IEnumerable<GitTagMetadata> items, TicketKey ticketKey)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(ticketKey);

        var table = new ConsoleTable("Tag", "Description");
        foreach (var tag in items)
        {
            var description = CreateDescription(tag, ticketKey);
            table.AddRow(tag.Tag, description);
        }

        table.Write();
    }

    private static string CreateDescription(GitTagMetadata tag, TicketKey ticketKey)
    {
        return tag.TryGetTaskIds(ticketKey, out var ids)
            ? string.Join(',', ids)
            : tag.Description;
    }
}