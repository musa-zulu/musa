namespace B2B.Application.Common.Interfaces;

public interface IIdempotencyService
{
    Task<Guid?> GetAsync(string key, string user);
    Task StoreAsync(string key, string user, Guid result);
}
