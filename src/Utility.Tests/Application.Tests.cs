using FluentAssertions;

using Moq;

using Abstractions;
using Utility.Console;

using Models;

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

    [Theory(DisplayName = "Run handles invalid command-line arguments")]
    [InlineData("--wrong", "argument")]
    [InlineData("", "")]
    [InlineData("--path")]
    [Trait("Category", "Unit")]
    public void RunHandlesInvalidCommandLineArguments(string arg1, string arg2 = null!)
    {
        // Arrange
        var args = arg2 == null ? new[] { arg1 } : new[] { arg1, arg2 };
        var mockReportBuilder = new Mock<IGitReportBuilder>(MockBehavior.Strict);
        var application = new Application(mockReportBuilder.Object);
        using var consoleOutput = new ConsoleOutput();

        // Act
        application.Run(args);

        // Assert
        consoleOutput.GetOutput().Should().Contain("Invalid command-line arguments.");
    }

    [Fact(DisplayName = "Run executes report build process successfully with valid arguments")]
    [Trait("Category", "Unit")]
    public void RunExecutesReportBuildSuccessfullyWithValidArguments()
    {
        // Arrange
        var validPath = Directory.GetCurrentDirectory();
        var args = new[] { "--path", validPath, "--key", "TASK-001" };
        var gitPath = new GitPath(validPath);
        var taskTag = new TicketKey("TASK-001");
        var mockReportBuilder = new Mock<IGitReportBuilder>(MockBehavior.Strict);
        var runCount = 0;
        mockReportBuilder.Setup(m => m.Build(It.Is<GitPath>(gp => gp.Value == gitPath.Value),
                                              It.Is<TicketKey>(tk => tk.Value == taskTag.Value)))
                         .Callback(() => runCount++);
        var application = new Application(mockReportBuilder.Object);

        // Act
        application.Run(args);

        // Assert
        runCount.Should().Be(1);
    }
}