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

    private const string NoDescription = "<No description>";
}