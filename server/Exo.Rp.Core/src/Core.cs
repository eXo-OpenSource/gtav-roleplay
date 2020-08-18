using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using AutoMapper;
using Exo.Rp.Core.AutoMapper;
using Exo.Rp.Core.BankAccounts;
using Exo.Rp.Core.Commands;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Environment;
using Exo.Rp.Core.Factories.BaseObjects;
using Exo.Rp.Core.Factories.Entities;
using Exo.Rp.Core.Inventory;
using Exo.Rp.Core.Inventory.Items;
using Exo.Rp.Core.Jobs;
using Exo.Rp.Core.Peds;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Plugins;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.StartupTasks;
using Exo.Rp.Core.StartupTasks.Tasks;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Grid;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Core.Teams;
using Exo.Rp.Core.Translation;
using Exo.Rp.Core.Updateable;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.World;
using Exo.Rp.Sdk.Logger;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sentry.Protocol;
using MetricsCollector = Exo.Rp.Core.Metrics.MetricsCollector;
using IPlayer = AltV.Net.Elements.Entities.IPlayer;

namespace Exo.Rp.Core
{
    public class Core : Resource
    {
        private static readonly Logger<Core> Logger = new Logger<Core>();
        private DatabaseCore _databaseCore;
        private static IServiceProvider _serviceProvider;

        private UpdateableManager _updateableManager;

        public override void OnStart()
        {
            // Initialize database
            _databaseCore = new DatabaseCore();

            // Prepare service provider
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IMapper>(AutoMapperConfiguration.GetMapper())
                .AddSingleton(typeof(ILogger), typeof(Logger))
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddSingleton<RuntimeIndexer>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<MetricsCollector>()
                .AddSingleton<TranslationManager>()
                .AddSingleton<PlayerManager>()
                .AddSingleton<TeamManager>()
                .AddSingleton<VehicleManager>()
                .AddSingleton<BankAccountManager>()
                .AddSingleton<ShopManager>()
                .AddSingleton<ItemManager>()
                .AddSingleton<InventoryManager>()
                .AddSingleton<IplManager>()
                .AddSingleton<JobManager>()
                .AddSingleton<UpdateableManager>()
                .AddSingleton<PluginManager>()
                .AddSingleton<PrivateStreamer>()
                .AddSingleton<PublicStreamer>()
                .AddSingleton<DoorManager>()
                .AddSingleton<EnvironmentManager>()
                .AddSingleton<PedManager>();

            // Add startup tasks
            serviceCollection
                .AddStartupTask<InitializeEntitySyncTask>()
                .AddStartupTask<LoadManagerTask>();


            // Start loading database models
            _databaseCore.OnResourceStartHandler(
                configureSentry: (settings, options) =>
                {
                    var logger = new SentryLogger(settings.LoggerLevel);
                    options.Dsn = settings.Dsn;
                    options.Environment = settings.Environment;
                    options.Release = settings.Release;
                    options.Debug = settings.Debug;
                    options.DiagnosticLogger = logger;
                    options.AttachStacktrace = true;
                    options.BeforeSend = e =>
                    {
                        logger.Log(SentryLevel.Info, "Sending event with Id {0}.", null, e.EventId);

                        return e;
                    };
                    options.ServerName = settings.Environment;
                },
                onDatabaseInitialized: () => {
                    _serviceProvider = serviceCollection
                        .AddSingleton(ContextFactory.Instance)
                        .BuildServiceProvider();
                    RunTasks().Wait();
                }
            );
        }

        private async Task RunTasks()
        {
            Logger.Info("Startup Tasks | Executing Tasks...");
            var stopWatch = Stopwatch.StartNew();

            var startupTasks = _serviceProvider.GetServices<IStartupTask>();
            foreach (var task in startupTasks)
            {
                Logger.Info($"Startup Tasks | Executing {task.GetType().Name}...");
                var _stopWatch = Stopwatch.StartNew();

                await task.ExecuteAsync();

                _stopWatch.Stop();
                Logger.Info($"Startup Tasks | Executed {task.GetType().Name} in {_stopWatch.ElapsedMilliseconds}ms.");
            }

            stopWatch.Stop();
            Logger.Debug($"Startup Tasks | Excuted Taks in {stopWatch.ElapsedMilliseconds} ms.");
        }

        public override void OnStop()
        {
            Logger.Info("Disposing Metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Dispose();
            Logger.Info("Disposing Plugin manager...");
            _serviceProvider.GetService<PluginManager>().Dispose();

            Logger.Info("Committing changes to database...");
            DatabaseCore.SaveChangeToDatabase();
            _databaseCore.OnResourceStopHandler();
        }

        public override void OnTick()
        {
            try
            {
                _updateableManager?.Tick();
            }
            catch (Exception e) // I think there will never land an exception here, but who knows :)
            {
                e.TrackOrThrow();
            }
        }

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new PlayerEntityFactory();
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new VehicleEntityFactory();
        }

        public override IBaseObjectFactory<IBlip> GetBlipFactory()
        {
            return base.GetBlipFactory();
        }

        public override IBaseObjectFactory<IColShape> GetColShapeFactory()
        {
            return new ColShapeBaseObjectFactory();
        }
        
        public static T GetService<T>()
            where T : IService
        {
            return _serviceProvider.GetService<T>();
        }

        public static object GetService(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }

    class Vector
    {
        [JsonProperty("X")]
        public float X { get; set; }

        [JsonProperty("Y")]
        public float Y { get; set; }

        [JsonProperty("Z")]
        public float Z { get; set; }
    }

    class Bin
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Position")]
        public Vector Position { get; set; }

        [JsonProperty("Rotation")]
        public Vector Rotation { get; set; }
    }
}