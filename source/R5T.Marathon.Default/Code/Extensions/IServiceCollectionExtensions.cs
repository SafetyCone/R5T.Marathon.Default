using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;



namespace R5T.Marathon.Default
{
    public static class IServiceCollectionExtensions
    {
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
        public static IServiceAction<IBackgroundWorkItemQueue> AddBackgroundWorkItemQueueAction(this IServiceCollection services)
        {
            var serviceAction = new ServiceAction<IBackgroundWorkItemQueue>(() => services.AddBackgroundWorkItemQueue());
            return serviceAction;
        }
    }
}
