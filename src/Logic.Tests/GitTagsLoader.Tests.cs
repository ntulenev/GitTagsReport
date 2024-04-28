using FluentAssertions;

using LibGit2Sharp;

using Moq;

using Models;

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
    public void LoadTagsCanLoadsData()
    {
        // Arrange
        var path = new GitPath(Directory.GetCurrentDirectory());
        var tagCollection = new Mock<TagCollection>(MockBehavior.Strict);
        var friendlyName = "Name123";
        var commitMessage = "Message123";
        var gitSha = "Test";
        var testTag = new Mock<Tag>(MockBehavior.Strict);
        var testGitObject = new Mock<GitObject>(MockBehavior.Strict);
        testGitObject.Setup(x => x.Sha).Returns(gitSha);
        testTag.Setup(x => x.Target).Returns(testGitObject.Object);
        testTag.Setup(x => x.FriendlyName).Returns(friendlyName);
        var tags = new List<Tag>()
        {
            testTag.Object
        };
        tagCollection.Setup(x => x.GetEnumerator())
            .Returns(() => ((IEnumerable<Tag>) tags).GetEnumerator());
        var repo = new Mock<IRepository>(MockBehavior.Strict);
        var disposeCount = 0;
        repo.Setup(x => x.Dispose()).Callback(() => disposeCount++);
        repo.Setup(x => x.Tags).Returns(tagCollection.Object);
        var commitsLog = new Mock<IQueryableCommitLog>(MockBehavior.Strict);
        var testCommit = new Mock<Commit>(MockBehavior.Strict);
        testCommit.Setup(x => x.Sha).Returns(gitSha);
        testCommit.Setup(x => x.MessageShort).Returns(commitMessage);
        var testCommits = new List<Commit>()
        {
            testCommit.Object
        };
        commitsLog.Setup(x => x.GetEnumerator())
            .Returns(() => ((IEnumerable<Commit>) testCommits).GetEnumerator());
        repo.Setup(x => x.Commits).Returns(commitsLog.Object);
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
        disposeCount.Should().Be(1);
        items[0].Tag.Should().Be(friendlyName);
        items[0].Description.Should().Be(commitMessage);
    }
}