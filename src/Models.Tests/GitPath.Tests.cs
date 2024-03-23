using FluentAssertions;

namespace Models.Tests;

public class GitPathTests
{
    [Fact(DisplayName = nameof(GitPath) + " constructor throws ArgumentException when path is null or whitespace")]
    [Trait("Category", "Unit")]
    public void ConstructorNullOrWhiteSpacePathThrowsArgumentException()
    {
        // Act
        Action act = () => _ = new GitPath(null!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact(DisplayName = nameof(GitPath) + " constructor throws ArgumentException when path does not exist")]
    [Trait("Category", "Unit")]
    public void ConstructorNonExistentPathThrowsArgumentException()
    {
        // Arrange
        var invalidPath = "nonexistentpath";

        // Act
        Action act = () => _ = new GitPath(invalidPath);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact(DisplayName = nameof(GitPath) + " constructor initializes instance when valid path is provided")]
    [Trait("Category", "Unit")]
    public void ConstructorValidPathInitializesInstance()
    {
        // Arrange
        var validPath = Directory.GetCurrentDirectory();

        // Act
        var gitPath = new GitPath(validPath);

        // Assert
        gitPath.Should().NotBeNull();
        gitPath.Value.Should().Be(validPath);
    }
}