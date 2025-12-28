using FluentAssertions;

namespace Models.Tests;

public class TicketKeyTests
{
    [Fact(DisplayName = nameof(TicketKey) + " constructor throws ArgumentException when ticket key is null " +
                        "or whitespace")]
    [Trait("Category", "Unit")]
    public void ConstructorThrowsArgumentExceptionWhenTicketKeyIsNullOrWhiteSpace()
    {
        // Act
        Action act = () => _ = new TicketKey(null!);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Tag cannot be null, empty, or whitespace.*");
    }

    [Fact(DisplayName = nameof(TicketKey) + " constructor initializes instance when valid ticket key is provided")]
    [Trait("Category", "Unit")]
    public void ConstructorInitializesInstanceWhenValidTicketKeyIsProvided()
    {
        // Arrange
        var validTicketKey = "AAA";

        // Act
        var ticketKey = new TicketKey(validTicketKey);

        // Assert
        ticketKey.Should().NotBeNull();
        ticketKey.Value.Should().Be(validTicketKey);
    }
}