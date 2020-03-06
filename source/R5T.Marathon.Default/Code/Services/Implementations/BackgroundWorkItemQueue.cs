using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


namespace R5T.Marathon.Default
{
    // Adapted from: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2&tabs=visual-studio#queued-background-tasks-1
    public class BackgroundWorkItemQueue : IBackgroundWorkItemQueue
    {
        private ConcurrentQueue<Func<IServiceProvider, CancellationToken, Task>> WorkItems { get; } = new ConcurrentQueue<Func<IServiceProvider, CancellationToken, Task>>();
        private SemaphoreSlim Signal { get; }  = new SemaphoreSlim(0);


        public async Task<Func<IServiceProvider, CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await this.Signal.WaitAsync(cancellationToken);

            this.WorkItems.TryDequeue(out var workItem);

            return workItem;
        }

        public void QueueBackgroundWorkItem(Func<IServiceProvider, CancellationToken, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            this.WorkItems.Enqueue(workItem);

            this.Signal.Release();
        }
    }
}
