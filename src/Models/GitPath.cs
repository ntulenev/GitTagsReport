namespace Models;

/// <summary>
/// Represents a path of a Git repository.
/// </summary>
public sealed class GitPath
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GitPath"/> class with a specified path.
    /// </summary>
    /// <param name="value">The file system path that represents a location of a Git repository.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null, empty, or whitespace,
    /// or when the path does not exist or is not a valid file or directory path.</exception>
    public GitPath(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Path cannot be null or whitespace.", nameof(value));
        }

        if (!Directory.Exists(value))
        {
            throw new ArgumentException("The path does not exist or is not a valid file or directory path.",
                nameof(value));
        }

        Value = value;
    }

    /// <summary>
    /// Gets the file system path value of the Git path.
    /// </summary>
    public string Value { get; }
}