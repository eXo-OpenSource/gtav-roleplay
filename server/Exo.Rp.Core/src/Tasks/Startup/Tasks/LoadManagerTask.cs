using System;
using System.Threading;
using System.Threading.Tasks;
using Exo.Rp.Core.BankAccounts;
using Exo.Rp.Core.Commands;
using Exo.Rp.Core.Environment;
using Exo.Rp.Core.Inventory;
using Exo.Rp.Core.Inventory.Items;
using Exo.Rp.Core.Jobs;
using Exo.Rp.Core.Metrics;
using Exo.Rp.Core.Peds;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Plugins;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Core.Tasks.StartupTasks;
using Exo.Rp.Core.Teams;
using Exo.Rp.Core.Translation;
using Exo.Rp.Core.Updateable;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.World;
using Exo.Rp.Sdk.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Exo.Rp.Core.Tasks.Startup.Tasks
{
    public class LoadManagerTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LoadManagerTask> _logger;

        public LoadManagerTask(IServiceProvider serviceProvider, ILogger<LoadManagerTask> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _logger.Info("Loading services...");

            _logger.Info("Services | Loading Streaming handler...");
            _serviceProvider.GetService<PublicStreamer>();
            _serviceProvider.GetService<PrivateStreamer>();

            _logger.Info("Services | Loading Command handler...");
            _serviceProvider.GetService<CommandHandler>();

            //_logger.Info("Services | Loading Metrics collector...");
            //_serviceProvider.GetService<MetricsCollector>().Start();

            _logger.Info("Services | Loading Translation manager...");
            _serviceProvider.GetService<TranslationManager>();

            _logger.Info("Services | Loading Player manager...");
            _serviceProvider.GetService<PlayerManager>();

            _logger.Info("Services | Loading Team manager...");
            _serviceProvider.GetService<TeamManager>();

            _logger.Info("Services | Loading Vehicle manager...");
            _serviceProvider.GetService<VehicleManager>();

            _logger.Info("Services | Loading Bank Account manager...");
            _serviceProvider.GetService<BankAccountManager>();

            _logger.Info("Services | Loading Shop manager...");
            _serviceProvider.GetService<ShopManager>();

            _logger.Info("Services | Loading Item manager...");
            _serviceProvider.GetService<ItemManager>();

            _logger.Info("Services | Loading Inventory manager...");
            _serviceProvider.GetService<InventoryManager>();

            _logger.Info("Services | Loading Ipl manager...");
            _serviceProvider.GetService<IplManager>();

            _logger.Info("Services | Loading Job manager...");
            _serviceProvider.GetService<JobManager>();

            _logger.Info("Services | Loading Cityhall manager...");
            _serviceProvider.GetService<CityhallManager>();

            _logger.Info("Services | Loading Updateable manager...");
            _serviceProvider.GetService<UpdateableManager>();

            _logger.Info("Services | Loading Door manager...");
            _serviceProvider.GetService<DoorManager>();

            _logger.Info("Services | Loading Environment manager...");
            _serviceProvider.GetService<EnvironmentManager>();

            _logger.Info("Services | Loading Ped manager...");
            _serviceProvider.GetService<PedManager>();

            _logger.Info("Services | Loading Plugin manager...");
            _serviceProvider.GetService<PluginManager>();

            return Task.CompletedTask;
        }
    }
}