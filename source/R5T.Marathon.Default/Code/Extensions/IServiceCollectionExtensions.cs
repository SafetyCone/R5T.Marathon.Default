using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;



namespace R5T.Marathon.Default
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueueProcessor"/> as a <see cref="Microsoft.Extensions.Hosting.IHostedService"/>.
        /// </summary>
        /// <remarks>
        /// No corresponding AddXAction() method for this service since the queue processor will never be injected.
        /// </remarks>
        public static IServiceCollection AddBackgroundWorkItemQueueProcessor(this IServiceCollection services,
            ServiceAction<IBackgroundWorkItemQueue> addBackgroundWorkItemQueue)
        {
            services
                .AddHostedService<BackgroundWorkItemQueueProcessor>()
                .RunServiceAction(addBackgroundWorkItemQueue)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueue"/> implmentation of <see cref="IBackgroundWorkItemQueue"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddBackgroundWorkItemQueue(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundWorkItemQueue, BackgroundWorkItemQueue>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="BackgroundWorkItemQueue"/> implmentation of <see cref="IBackgroundWorkItemQueue"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static ServiceAction<IBackgroundWorkItemQueue> AddBackgroundWorkItemQueueAction(this IServiceCollection services)
        {
            var serviceAction = new ServiceAction<IBackgroundWorkItemQueue>(() => services.AddBackgroundWorkItemQueue());
            return serviceAction;
        }
    }
}
