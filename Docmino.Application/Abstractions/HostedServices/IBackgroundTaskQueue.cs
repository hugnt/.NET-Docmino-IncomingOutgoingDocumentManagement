namespace Docmino.Application.Abstractions.HostedServices;
public interface IBackgroundTaskQueue<T>
{
    ValueTask QueueBackgroundWorkItemAsync(T workItem, CancellationToken cancellationToken = default);

    ValueTask<T> DequeueBackgroundWorkItemAsync(
        CancellationToken cancellationToken = default);
}