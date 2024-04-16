using System.Collections.Frozen;
using Abstractions;
using LibGit2Sharp;
using Models;

namespace Logic;

/// <summary>
/// Implements <see cref="IGitTagsLoader"/> to load Git tag metadata from a repository.
/// </summary>
/// <remarks>
/// This class uses LibGit2Sharp to access and read tags from a Git repository, 
/// transforming them into a collection of <see cref="GitTagMetadata"/>.
/// </remarks>
public class GitTagsLoader : IGitTagsLoader
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GitTagsLoader"/> class.
    /// </summary>
    /// <param name="repoFactory">A function that creates a Git repository given a <see cref="GitPath"/>.</param>
    /// <remarks>
    /// This constructor initializes a new instance of the <see cref="GitTagsLoader"/> class with the
    /// provided repository factory function.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="repoFactory"/> is null.</exception>
    public GitTagsLoader(Func<GitPath, IRepository> repoFactory)
    {
        ArgumentNullException.ThrowIfNull(repoFactory);
        _repoFactory = repoFactory;
    }

    /// <inheritdoc/>
    public IEnumerable<GitTagMetadata> LoadTags(GitPath path)
    {
        ArgumentNullException.ThrowIfNull(path);

        using var repo = _repoFactory(path);
        var tags = repo.Tags.ToFrozenDictionary(x => x.Target.Sha);
        foreach (var commit in repo.Commits)
        {
            if (tags.TryGetValue(commit.Sha, out var targetTag))
            {
                yield return new GitTagMetadata(targetTag.FriendlyName, commit.MessageShort);
            }
        }
    }

    private readonly Func<GitPath, IRepository> _repoFactory;
}