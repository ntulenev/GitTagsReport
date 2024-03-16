using Models;

namespace Abstractions;

public interface IGitReportPrinter
{
    public void Print(IEnumerable<GitTagMetadata> items,TicketKey taskFilter);
}