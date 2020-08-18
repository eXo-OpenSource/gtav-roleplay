using Microsoft.Extensions.DependencyInjection;

namespace Exo.Rp.Core.StartupTasks
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();
    }
}