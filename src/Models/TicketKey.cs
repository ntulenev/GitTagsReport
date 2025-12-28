namespace Models;

/// <summary>
/// Represents a ticket key in a task tracker system (e.g., Jira).
/// The ticket key is a prefix used to uniquely identify tickets within the system,
/// such as "AAA" in "AAA-123".
/// </summary>
public sealed class TicketKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TicketKey"/> class with the specified ticket key.
    /// </summary>
    /// <param name="ticketKey">The ticket key as a string. Must not be null, empty, or consist
    /// only of white-space characters.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="ticketKey"/> is null, empty,
    /// or whitespace.</exception>
    public TicketKey(string ticketKey)
    {
        if (string.IsNullOrWhiteSpace(ticketKey))
        {
            throw new ArgumentException("Tag cannot be null, empty, or whitespace.", nameof(ticketKey));

        }

        Value = ticketKey;
    }

    /// <summary>
    /// Gets the ticket key value.
    /// </summary>
    /// <value>The ticket key as a string.</value>
    public string Value { get; }
}