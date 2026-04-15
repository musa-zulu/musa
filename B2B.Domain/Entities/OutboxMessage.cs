namespace B2B.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
}
