using FluentAssertions;

using Moq;

using Abstractions;
using Utility.Console;

namespace Utility.Tests;

public class ApplicationTests
{
    [Fact(DisplayName = nameof(Application) + " throws ArgumentNullException " +
        "for null IGitReportBuilder")]
    [Trait("Category", "Unit")]
    public void ConstructorThrowsArgumentNullExceptionIfGitReportBuilderIsNull()
    {
        // Arrange & Act
        Action act = () => _ = new Application(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact(DisplayName = nameof(Application) + " can be created with valid params")]
    [Trait("Category", "Unit")]
    public void ApplicationCanBeCreatedWithValidParams()
    {
        // Arrange
        var mockReportBuilder = new Mock<IGitReportBuilder>(MockBehavior.Strict);

        // Act
        var ex = Record.Exception(() => _ = new Application(mockReportBuilder.Object));

        // Assert
        ex.Should().BeNull();
    }
}