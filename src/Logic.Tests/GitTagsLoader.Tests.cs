using FluentAssertions;
using LibGit2Sharp;
using Moq;

namespace Logic.Tests;

public class GitTagsLoaderTests
{
    [Fact(DisplayName = nameof(GitTagsLoader) + " throws ArgumentNullException when repoFactory is null")]
    [Trait("Category", "Unit")]
    public void ConstructorThrowsArgumentNullExceptionWhenRepoFactoryIsNull()
    {
        // Act
        Action act = () => _ = new GitTagsLoader(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact(DisplayName = "LoadTags throws ArgumentNullException when path is null")]
    [Trait("Category", "Unit")]
    public void LoadTagsThrowsArgumentNullExceptionWhenPathIsNull()
    {
        // Arrange
        var loader = new GitTagsLoader(_ => new Mock<IRepository>(MockBehavior.Strict).Object);

        // Act
        var ex = Record.Exception(() => _ = loader.LoadTags(null!).ToList());

        // Assert
        ex.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}