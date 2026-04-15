namespace B2B.Application.Common.Interfaces;

public interface IOutboxService
{
    Task AddAsync(string type, string payload);
}
