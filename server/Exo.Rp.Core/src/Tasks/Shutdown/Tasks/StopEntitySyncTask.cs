using AltV.Net.EntitySync;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Sdk.Logger;
using System.Threading;
using System.Threading.Tasks;

namespace Exo.Rp.Core.Tasks.Shutdown.Tasks
{
    public class StopEntitySyncTask : IShutdownTask
    {
        
        private readonly PrivateStreamer _privateStreamer;
        private readonly ILogger<StopEntitySyncTask> _logger;

        public StopEntitySyncTask(PrivateStreamer privateStreamer, ILogger<StopEntitySyncTask> logger)
        {
            _privateStreamer = privateStreamer;
            _logger = logger;
        }
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _logger.Info("Services | Stopping Entity Sync...");
            AltEntitySync.Stop();
            
            _logger.Info("Services | Stopping Private Entity Sync...");
            _privateStreamer.Stop();
            
            return Task.CompletedTask;
        }
    }
}