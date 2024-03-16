using Models;

namespace Abstractions;

public interface IGitReportBuilder
{
    public void Build(GitPath path,TicketKey ticketKey);
}