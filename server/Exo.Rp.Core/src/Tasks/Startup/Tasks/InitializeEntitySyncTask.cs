using System.Threading;
using System.Threading.Tasks;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Grid;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Core.Tasks.StartupTasks;

namespace Exo.Rp.Core.Tasks.Startup.Tasks
{
    public class InitializeEntitySyncTask : IStartupTask
    {
        private readonly PrivateStreamer _privateStreamer;

        public InitializeEntitySyncTask(PrivateStreamer privateStreamer)
        {
            _privateStreamer = privateStreamer;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
           _privateStreamer.Init(
                1,
                500,
                (threadCount, repo) => new BetterServerEventNetworkLayer(threadCount, repo),
                (entity, threadCount) => entity.Type,
                (entityId, entityType, threadCount) => entityType,
                (threadid) => threadid switch {
                        _ => new LimitedPrivateGrid3(50_000, 50_000, 75, 10_000, 10_000, 500)
                    }, new IdProvider());

            AltEntitySync.Init(5, id => 100, _ => false,
                (threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository),
                (entity, threadCount) => entity.Type,
                (entityId, entityType, threadCount) => entityType,
                (threadId) => threadId switch
                    {
                        1 => new LimitedGrid3(50_000, 50_000, 125, 10_000, 10_000, 1000),
                        2 => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 128),
                        _ => new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 300)
                    }, _privateStreamer.idProvider);

            return Task.CompletedTask;
        }
    }
}