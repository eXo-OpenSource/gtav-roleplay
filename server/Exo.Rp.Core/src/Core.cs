using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
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
using Exo.Rp.Core.Sentry;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Core.Tasks;
using Exo.Rp.Core.Tasks.Shutdown;
using Exo.Rp.Core.Tasks.Startup.Tasks;
using Exo.Rp.Core.Tasks.StartupTasks;
using Exo.Rp.Core.Teams;
using Exo.Rp.Core.Translation;
using Exo.Rp.Core.Updateable;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Core.Util.Settings;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.World;
using Exo.Rp.Sdk.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Sentry;
using MetricsCollector = Exo.Rp.Core.Metrics.MetricsCollector;
using IPlayer = AltV.Net.Elements.Entities.IPlayer;

namespace Exo.Rp.Core
{
    public class Core : Resource
    {
        private static readonly Logger<Core> Logger = new Logger<Core>();
        private static IHost _host;
        private DatabaseCore _databaseCore;

        private UpdateableManager _updateableManager;

        public async override void OnStart()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime()
                .Build();

            await _host.StartAsync();
        }

        private async void ConfigureServices(HostBuilderContext context, IServiceCollection collection)
        {
            collection.AddSingleton<IMapper>(AutoMapperConfiguration.GetMapper())
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
            collection.AddStartupTask<InitializeEntitySyncTask>()
                .AddStartupTask<LoadManagerTask>();
            
            // Load the settings
            var settingsPath = Path.Combine("resources", Alt.Server.Resource.Name, "config.json");
            var logsPath = Path.Combine("resources", Alt.Server.Resource.Name, "logs");
            if (!SettingsManager.LoadSettings(settingsPath))
            {
                Logger.Error($"Unable to load settings from \"{settingsPath}\". Trying to create a new one.");
                if (!SettingsManager.CreateSettings(settingsPath, logsPath))
                {
                    Logger.Fatal("Unable to create a new settings file.");
                    throw null;
                }
            }
            else
                Logger.Debug($"Successfully loaded settings from \"{settingsPath}\".");
            
            // Initialize sentry
            if (SettingsManager.ServerSettings.Sentry.Release.Length > 0)
                 SentrySdk.Init((options) =>
                 {
                     var settings = SettingsManager.ServerSettings.Sentry;
                     var logger = new SentryLogger(settings.LoggerLevel);
                     options.Dsn = settings.Dsn;
                     options.Environment = settings.Environment;
                     options.Release = settings.Release;
                     options.Debug = settings.Debug;
                     options.DiagnosticLogger = logger;
                     options.AttachStacktrace = true;
                     options.SendDefaultPii = true;
                     options.AddExceptionProcessor(new SentryEventExceptionProcessor(new Logger<SentryEventExceptionProcessor>()));
                     options.ServerName = settings.Environment;
                 });
            
            // Initialize database
            _databaseCore = new DatabaseCore();
            _databaseCore.CreateDatabaseConnection();
            collection.AddSingleton(ContextFactory.Connect());
            
            // Start loading database models
            await _databaseCore.OnResourceStartHandler();
            await ExecuteTasks<IStartupTask>( "Startup");
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

        public async override void OnStop()
        {
            await ExecuteTasks<IShutdownTask>("Shutdown");
            
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
            return _host.Services.GetService<T>();
        }

        public static object GetService(Type type)
        {
            return _host.Services.GetService(type);
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