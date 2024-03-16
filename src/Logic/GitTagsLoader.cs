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
    /// <inheritdoc/>
    public IEnumerable<GitTagMetadata> LoadTags(GitPath path)
    {
        ArgumentNullException.ThrowIfNull(path);

        using var repo = new Repository(path.Value);
        var tags = repo.Tags.ToFrozenDictionary(x => x.Target.Sha);
        foreach (var commit in repo.Commits)
        {
            if (tags.TryGetValue(commit.Sha, out var targetTag))
            {
                yield return new GitTagMetadata(targetTag.FriendlyName, commit.MessageShort);
            }
        }
    }
}