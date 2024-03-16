using System.Collections.Frozen;
using System.Collections.Immutable;
using Abstractions;
using LibGit2Sharp;
using Models;

namespace Logic;

public class GitTagsLoader : IGitTagsLoader
{
    public IEnumerable<GitTagMetadata> LoadTags(GitPath path)
    {
        ArgumentNullException.ThrowIfNull(path);

        using var repo = new Repository(path.Path);
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