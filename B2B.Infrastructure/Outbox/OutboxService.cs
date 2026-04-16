using B2B.Application.Common.Interfaces;
using B2B.Infrastructure.Common.Persistence;

namespace B2B.Infrastructure.Outbox;

public class OutboxService(AppDbContext _appDbContext, IDateTimeProvider _dateTimeProvider) : IOutboxService
{
    public async Task AddAsync(string type, string payload)
    {
        _appDbContext.OutboxMessages.Add(new Domain.Entities.OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = type,
            Payload = payload,
            CreateDate = _dateTimeProvider.UtcNow
        });

        await _appDbContext.SaveChangesAsync();
    }
}
