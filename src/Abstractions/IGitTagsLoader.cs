using Models;

namespace Abstractions;

/// <summary>
/// Defines the contract for a service responsible for loading Git tag metadata from a specified repository.
/// </summary>
public interface IGitTagsLoader
{
    /// <summary>
    /// Loads and returns Git tag metadata from the repository located at the specified path.
    /// </summary>
    /// <param name="path">The path to the Git repository from which to load tag metadata.</param>
    /// <returns>
    /// An enumerable collection of <see cref="GitTagMetadata"/> objects, each representing metadata for a single Git tag
    /// found in the specified repository.
    /// </returns>
    IEnumerable<GitTagMetadata> LoadTags(GitPath path);
}
