namespace B2B.Domain.Entities;

public class IdempotencyRecord
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public Guid? ResultId { get; set; }
    public DateTime CreatedAt { get; set; }
}
