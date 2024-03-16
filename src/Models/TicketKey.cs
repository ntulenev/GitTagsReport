namespace Models;

public sealed class TicketKey
{
    public TicketKey(string ticketKey)
    {
        if (string.IsNullOrWhiteSpace(ticketKey))
        {
            throw new ArgumentException("Tag cannot be null, empty, or whitespace.", nameof(ticketKey));
            
        }

        Value = ticketKey;
    }
    
    public string Value { get; }
}