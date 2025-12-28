using FluentAssertions;

using Moq;

using Abstractions;
using Models;

namespace Logic.Tests;

public class GitReportBuilderTests
{
    [Fact(DisplayName = nameof(GitReportBuilder) + " throws ArgumentNullException when loader is null")]
    [Trait("Category", "Unit")]
    public void ConstructorThrowsArgumentNullExceptionWhenLoaderIsNull()
    {
        // Arrange
        var printerMock = new Mock<IGitReportPrinter>(MockBehavior.Strict).Object;

        // Act
        Action act = () => _ = new GitReportBuilder(null!, printerMock);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact(DisplayName = nameof(GitReportBuilder) + " throws ArgumentNullException when printer is null")]
    [Trait("Category", "Unit")]
    public void ConstructorThrowsArgumentNullExceptionWhenPrinterIsNull()
    {
        // Arrange
        var loaderMock = new Mock<IGitTagsLoader>(MockBehavior.Strict).Object;

        // Act
        Action act = () => _ = new GitReportBuilder(loaderMock, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact(DisplayName = "Build calls LoadTags and Print with correct parameters")]
    [Trait("Category", "Unit")]
    public void BuildCallsLoadTagsAndPrintWithCorrectParameters()
    {
        // Arrange
        var currentPath = Directory.GetCurrentDirectory();
        var path = new GitPath(currentPath);
        var taskFilter = new TicketKey("TASK-123");
        var tags = new[] { new GitTagMetadata("v1.0", "Description") };
        var loaderCount = 0;
        var loaderMock = new Mock<IGitTagsLoader>(MockBehavior.Strict);
        loaderMock.Setup(loader => loader.LoadTags(path))
            .Returns(tags)
            .Callback(() => loaderCount++);

        var printerCount = 0;
        var printerMock = new Mock<IGitReportPrinter>(MockBehavior.Strict);
        printerMock.Setup(printer => printer.Print(tags, taskFilter))
            .Callback(() => printerCount++);

        var gitReportBuilder = new GitReportBuilder(loaderMock.Object, printerMock.Object);

        // Act
        gitReportBuilder.Build(path, taskFilter);

        // Assert
        loaderCount.Should().Be(1);
        printerCount.Should().Be(1);
    }
}