using Exo.Rp.Core.Tasks.Shutdown;
using Exo.Rp.Core.Tasks.StartupTasks;
using Microsoft.Extensions.DependencyInjection;

namespace Exo.Rp.Core.Tasks
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTask<T, TImpl>(this IServiceCollection collection)
            where T : class, ITask
            where TImpl : class, T
            => collection.AddTransient<T, TImpl>();

        public static IServiceCollection AddStartupTask<T>(this IServiceCollection collection)
            where T : class, IStartupTask
            => AddTask<IStartupTask, T>(collection);

        public static IServiceCollection AddShutdownTask<T>(this IServiceCollection collection)
            where T : class, IShutdownTask
            => AddTask<IShutdownTask, T>(collection);
    }
}