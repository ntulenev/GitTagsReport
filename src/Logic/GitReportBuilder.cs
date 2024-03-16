using Abstractions;
using Models;

namespace Logic;

/// <summary>
/// Builds and outputs Git reports based on tag metadata and a specified task filter.
/// </summary>
public sealed class GitReportBuilder : IGitReportBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GitReportBuilder"/>
    /// class with specified loader and printer services.
    /// </summary>
    /// <param name="loader">The service used to load Git tag metadata.</param>
    /// <param name="printer">The service used to print the report.</param>
    /// <exception cref="ArgumentNullException">Thrown if either <paramref name="loader"/>
    /// or <paramref name="printer"/> is null.</exception>
    public GitReportBuilder(IGitTagsLoader loader, IGitReportPrinter printer)
    {
        ArgumentNullException.ThrowIfNull(loader);
        ArgumentNullException.ThrowIfNull(printer);

        _loader = loader;
        _printer = printer;
    }
    
    /// <inheritdoc/>
    public void Build(GitPath path, TicketKey taskFilter)
    {
        var tags = _loader.LoadTags(path);
        _printer.Print(tags,taskFilter);
    }

    private readonly IGitTagsLoader _loader;
    private readonly IGitReportPrinter _printer;
}