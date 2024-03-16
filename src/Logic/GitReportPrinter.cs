using System.Text.RegularExpressions;
using Abstractions;
using ConsoleTables;
using Models;

namespace Logic;

public sealed class GitReportPrinter : IGitReportPrinter
{
    public void Print(IEnumerable<GitTagMetadata> items, TicketKey ticketKey)
    {
        var pattern = new Regex(@$"{ticketKey.Value}-\d+");

        var table = new ConsoleTable("Tag", "Description");
        foreach (var tag in items)
        {
            var taskId = SearchKey(tag.Description);
            table.AddRow(tag.Tag, taskId);
        }
        table.Write();

        string SearchKey(string description) => pattern.Match(description).Value;
    }
}