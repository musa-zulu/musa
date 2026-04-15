namespace B2B.Application.Common.Interfaces;

public interface IMessageQueue
{
    Task PublishAsync<T>(T message);
}
