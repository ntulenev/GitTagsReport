using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Models;

/// <summary>
/// Represents metadata for a Git tag, including the tag itself and an optional description.
/// </summary>
public sealed class GitTagMetadata
{
    /// <summary>
    /// Gets the tag name.
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Gets the description of the tag. If no description is provided, a default "No description" is used.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GitTagMetadata"/> class with
    /// a specified tag and an optional description.
    /// </summary>
    /// <param name="tag">The name of the tag. Cannot be null, empty, or consist only
    /// of white-space characters.</param>
    /// <param name="description">The description of the tag. If null, a default
    /// description is used.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="tag"/> i
    /// s null, empty, or consists only of white-space characters.</exception>
    public GitTagMetadata(string tag, string description)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            throw new ArgumentException("Tag cannot be null, empty, or whitespace.", nameof(tag));
        }

        Tag = tag;
        Description = description ?? NoDescription;
    }

    /// <summary>
    /// Attempts to extract task IDs from a ticket key based on a predefined pattern.
    /// </summary>
    /// <param name="ticketKey">The ticket key from which to extract task IDs.</param>
    /// <param name="Ids">When this method returns, contains an enumerable of extracted task IDs, 
    /// if any are found; otherwise, the default value for the type if no matches are found.</param>
    /// <returns><c>true</c> if the pattern matches and task IDs are successfully extracted; otherwise,
    /// <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="ticketKey"/> is null.</exception>
    public bool TryGetTaskIds(TicketKey ticketKey, [MaybeNullWhen(false)] out IEnumerable<string> Ids)
    {
        ArgumentNullException.ThrowIfNull(ticketKey);

        var pattern = new Regex(@$"{ticketKey.Value}-\d+");
        var matches = pattern.Matches(Description);
        if (matches.Any())
        {
            Ids = matches.Select(x => x.Value).Distinct();
            return true;
        }

        Ids = default;

        return false;
    }

    private const string NoDescription = "<No description>";
}


/*


        var table = new ConsoleTable("Tag", "Description");
        foreach (var tag in items)
        {
            var taskId = SearchKey(tag.Description);
            table.AddRow(tag.Tag, taskId);
        }
        table.Write();

        string SearchKey(string description) => pattern.Match(description).Value;
 */