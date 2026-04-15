using B2B.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace B2B.Infrastructure.Common.Persistence;

public class UnitOfWork(
    AppDbContext _appDbContext,
    IDbContextTransaction? _tx) : IUnitOfWork
{
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
       => _tx = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_tx is not null)
            await _appDbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_tx is not null)
            await _tx.RollbackAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _appDbContext.SaveChangesAsync(cancellationToken);    
}
