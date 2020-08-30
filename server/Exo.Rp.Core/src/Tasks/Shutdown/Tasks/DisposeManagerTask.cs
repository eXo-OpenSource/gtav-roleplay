using System;
using System.Threading;
using System.Threading.Tasks;
using Exo.Rp.Core.Metrics;
using Exo.Rp.Core.Plugins;
using Exo.Rp.Sdk.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Exo.Rp.Core.Tasks.Shutdown.Tasks
{
    public class DisposeManagerTask : IShutdownTask
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DisposeManagerTask> _logger;

        public DisposeManagerTask(IServiceProvider serviceProvider, ILogger<DisposeManagerTask> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _logger.Info("Services | Disposing Metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Start();
            
            _logger.Info("Services | Disposing Plugin manager...");
            _serviceProvider.GetService<PluginManager>().Dispose();
            
            return Task.CompletedTask;
        }
    }
}