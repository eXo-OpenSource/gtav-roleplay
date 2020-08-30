using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Tasks;
using Exo.Rp.Core.Tasks.Shutdown;
using Exo.Rp.Core.Tasks.StartupTasks;
using Exo.Rp.Core.Util.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Exo.Rp.Core
{
    public class Core : IHostedService
    {
        private static IHost _host;

        public Core(IHost host)
        {
            _host = host;
        }

        private static readonly Logger<Core> Logger = new Logger<Core>();

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ExecuteTasks<IStartupTask>("Startup");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await ExecuteTasks<IShutdownTask>("Shutdown");
        }

        public static async Task ExecuteTasks<TTask>(string target = "Runtime")
            where TTask : ITask
        {
            Logger.Info($"Tasks | { target } | Executing Tasks...");
            var stopWatch = Stopwatch.StartNew();
            
            foreach (var task in _host.Services.GetServices<TTask>())
            {
                Logger.Debug($"Tasks | { target } | Executing {task.GetType().Name}...");
                var _stopWatch = Stopwatch.StartNew();

                await task.ExecuteAsync();

                _stopWatch.Stop();
                Logger.Debug($"Tasks | { target } | Executed {task.GetType().Name} in {_stopWatch.ElapsedMilliseconds}ms.");
            }

            stopWatch.Stop();
            Logger.Info($"Tasks | { target } | Excuted Taks in {stopWatch.ElapsedMilliseconds} ms.");
        }

        public static T GetService<T>()
            where T : IService
        {
            return _host.Services.GetService<T>();
        }

        public static object GetService(Type type)
        {
            return _host.Services.GetService(type);
        }
    }
}