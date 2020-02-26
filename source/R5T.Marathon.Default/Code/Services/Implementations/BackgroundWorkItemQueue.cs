using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


namespace R5T.Marathon.Default
{
    // Adapted from: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2&tabs=visual-studio#queued-background-tasks-1
    public class BackgroundWorkItemQueue : IBackgroundWorkItemQueue
    {
        private ConcurrentQueue<Func<CancellationToken, Task>> WorkItems { get; } = new ConcurrentQueue<Func<CancellationToken, Task>>();
        private SemaphoreSlim Signal { get; }  = new SemaphoreSlim(0);


        public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await this.Signal.WaitAsync(cancellationToken);

            this.WorkItems.TryDequeue(out var workItem);

            return workItem;
        }

        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
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
