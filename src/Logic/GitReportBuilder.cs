using Abstractions;
using Models;

namespace Logic;

public sealed class GitReportBuilder : IGitReportBuilder
{
    public GitReportBuilder(IGitTagsLoader loader, IGitReportPrinter printer)
    {
        ArgumentNullException.ThrowIfNull(loader);
        ArgumentNullException.ThrowIfNull(printer);

        _loader = loader;
        _printer = printer;
    }

    public void Build(GitPath path, TicketKey taskFilter)
    {
        var tags = _loader.LoadTags(path);
        _printer.Print(tags,taskFilter);
    }

    private readonly IGitTagsLoader _loader;
    private readonly IGitReportPrinter _printer;
}