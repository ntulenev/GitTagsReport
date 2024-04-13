using FluentAssertions;

namespace Models.Tests;

public class GitTagMetadataTests
{
    [Fact(DisplayName = nameof(GitTagMetadata) + " constructor throws ArgumentException when tag is null " +
                        "or whitespace")]
    [Trait("Category", "Unit")]
    public void ConstructorThrowsArgumentExceptionWhenTagIsNullOrWhiteSpace()
    {
        // Act
        Action act = () => _ = new GitTagMetadata(null!, "Description");

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Tag cannot be null, empty, or whitespace.*");
    }

    [Fact(DisplayName = nameof(GitTagMetadata) + " constructor initializes instance when valid tag is provided")]
    [Trait("Category", "Unit")]
    public void ConstructorInitializesInstanceWhenValidTagIsProvided()
    {
        // Arrange
        var validTag = "v1.0";

        // Act
        var gitTagMetadata = new GitTagMetadata(validTag, "Description");

        // Assert
        gitTagMetadata.Should().NotBeNull();
        gitTagMetadata.Tag.Should().Be(validTag);
        gitTagMetadata.Description.Should().Be("Description");
    }

    [Fact(DisplayName = nameof(GitTagMetadata) + " constructor initializes instance when valid tag is provided" +
                        "but no description provided")]
    [Trait("Category", "Unit")]
    public void ConstructorInitializesInstanceWhenValidTagIsProvidedButNullDescription()
    {
        // Arrange
        var validTag = "v1.0";

        // Act
        var gitTagMetadata = new GitTagMetadata(validTag, null!);

        // Assert
        gitTagMetadata.Should().NotBeNull();
        gitTagMetadata.Tag.Should().Be(validTag);
    }

    [Fact(DisplayName = nameof(GitTagMetadata) + " TryGetTaskIds returns false when no task IDs are found")]
    [Trait("Category", "Unit")]
    public void TryGetTaskIdsReturnsFalseWhenNoTaskIDsAreFound()
    {
        // Arrange
        var ticketKey = new TicketKey("KEY");

        var gitTagMetadata = new GitTagMetadata("v1.0", "No task IDs in description");

        // Act
        var result = gitTagMetadata.TryGetTaskIds(ticketKey, out var taskIds);

        // Assert
        result.Should().BeFalse();
        taskIds.Should().BeNull();
    }

    [Fact(DisplayName = nameof(GitTagMetadata) + " TryGetTaskIds returns true and extracts task IDs when task IDs " +
                        "are found")]
    [Trait("Category", "Unit")]
    public void TryGetTaskIdsReturnsTrueAndExtractsTaskIDsWhenTaskIDsAreFound()
    {
        // Arrange
        var ticketKey = new TicketKey("KEY");

        var description = "Task IDs: KEY-123, KEY-456";
        var gitTagMetadata = new GitTagMetadata("v1.0", description);

        // Act
        var result = gitTagMetadata.TryGetTaskIds(ticketKey, out var taskIds);

        // Assert
        result.Should().BeTrue();
        taskIds.Should().NotBeNull().And.HaveCount(2).And.ContainInOrder("KEY-123", "KEY-456");
    }
}