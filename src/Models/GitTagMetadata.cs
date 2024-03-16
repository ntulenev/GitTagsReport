namespace Models;

public sealed class GitTagMetadata
{
    public string Tag { get; }

    public string Description { get; }

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