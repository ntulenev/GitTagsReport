namespace Models;

public sealed class GitPath
{
    public GitPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be null or whitespace.", nameof(path));
        }

        if (!Directory.Exists(path))
        {
            throw new ArgumentException("The path does not exist or is not a valid file or directory path.",
                nameof(path));
        }

        Path = path;
    }

    public string Path { get; }
}