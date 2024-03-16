using Models;

namespace Abstractions;

public interface IGitTagsLoader
{
    public IEnumerable<GitTagMetadata> LoadTags(GitPath path);
}