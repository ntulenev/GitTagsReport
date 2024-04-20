using FluentAssertions;
using LibGit2Sharp;
using Logic.Tests.TestTypes;
using Models;
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

    [Fact(DisplayName = "LoadTags can load data")]
    [Trait("Category", "Unit")]
    public void LoadTagsLoadsData()
    {
        // Arrange
        var path = new GitPath(Directory.GetCurrentDirectory());
        var tagCollection = new Mock<TagCollection>(MockBehavior.Strict);
        var tags = new List<Tag>()
        {
            new TestTag()
        };
        tagCollection.Setup(x => x.GetEnumerator())
            .Returns(() => ((IEnumerable<Tag>) tags).GetEnumerator());
        var repo = new Mock<IRepository>(MockBehavior.Strict);
        var dispCount = 0;
        repo.Setup(x => x.Dispose()).Callback(() => dispCount++);
        repo.Setup(x => x.Tags).Returns(tagCollection.Object);
        var commitsLog = new TestCommitLog();
        repo.Setup(x => x.Commits).Returns(commitsLog);
        var loader = new GitTagsLoader(p =>
        {
            if (p == path)
            {
                return repo.Object;
            }

            return null!;
        });

        // Act
        var items = loader.LoadTags(path).ToList();

        // Assert
        items.Should().HaveCount(1);
        dispCount.Should().Be(1);
        items[0].Tag.Should().Be("Name123");
        items[0].Description.Should().Be("Message123");
    }
}