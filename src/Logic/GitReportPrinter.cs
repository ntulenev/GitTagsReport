using ConsoleTables;

using Abstractions;
using Models;

namespace Logic;

/// <summary>
/// Prints Git tag reports to the console.
/// </summary>
public sealed class GitReportPrinter : IGitReportPrinter
{
    /// <inheritdoc/>
    public void Print(IEnumerable<GitTagMetadata> items, TicketKey taskFilter)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(taskFilter);

        var table = new ConsoleTable("Tag", "Description");
        foreach (var tag in items)
        {
            var description = CreateDescription(tag, taskFilter);
            _ = table.AddRow(tag.Tag, description);
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