using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sentry;
using Sentry.Protocol;
using server.AutoMapper;
using server.BankAccounts;
using server.Commands;
using server.Database;
using server.Environment;
using server.Factories.Entities;
using server.Inventory;
using server.Inventory.Items;
using server.Jobs;
using server.Players;
using server.Shops;
using server.Teams;
using server.Translation;
using server.Updateable;
using server.Util;
using server.Util.Log;
using server.Vehicles;
using MetricsCollector = server.Metrics.MetricsCollector;
using IPlayer = AltV.Net.Elements.Entities.IPlayer;

namespace server
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
                .AddSingleton<PluginManager.PluginManager>();

            // Start loading database mode/ls
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
                    LoadServices();
                }
            ); 
        }

        private void LoadServices()
        {
            Logger.Info("Loading services...");
            var stopWatch = Stopwatch.StartNew();

            Logger.Info("Services | Loading Command handler...");
            _serviceProvider.GetService<CommandHandler>();
            Logger.Info("Services | Loading Metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Start();
            Logger.Info("Services | Loading Translation manager...");
            _serviceProvider.GetService<TranslationManager>();
            Logger.Info("Services | Loading Player manager...");
            _serviceProvider.GetService<PlayerManager>();
            Logger.Info("Services | Loading Team manager...");
            _serviceProvider.GetService<TeamManager>();
            Logger.Info("Services | Loading Vehicle manager...");
            _serviceProvider.GetService<VehicleManager>();
            Logger.Info("Services | Loading Bank Account manager...");
            _serviceProvider.GetService<BankAccountManager>();
            Logger.Info("Services | Loading Shop manager...");
            _serviceProvider.GetService<ShopManager>();
            Logger.Info("Services | Loading Item manager...");
            _serviceProvider.GetService<ItemManager>();
            Logger.Info("Services | Loading Inventory manager...");
            _serviceProvider.GetService<InventoryManager>();
            Logger.Info("Services | Loading Ipl manager...");
            _serviceProvider.GetService<IplManager>();
            Logger.Info("Services | Loading Job manager...");
            _serviceProvider.GetService<JobManager>();
            Logger.Info("Services | Loading Updateable manager...");
            _updateableManager = _serviceProvider.GetService<UpdateableManager>();
            Logger.Info("Services | Loading Plugin manager...");
            _serviceProvider.GetService<PluginManager.PluginManager>();

            stopWatch.Stop();
            Logger.Debug($"Loaded services in {stopWatch.ElapsedMilliseconds} ms.");
        }
        
        public override void OnStop()
        {
            Logger.Info("Stopping metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Dispose();
            _serviceProvider.GetService<PluginManager.PluginManager>().Dispose();

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
            return base.GetColShapeFactory();
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
}