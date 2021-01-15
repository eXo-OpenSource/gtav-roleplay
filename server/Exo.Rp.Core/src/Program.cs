using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AutoMapper;
using Exo.Rp.Core.AutoMapper;
using Exo.Rp.Core.BankAccounts;
using Exo.Rp.Core.Commands;
using Exo.Rp.Core.Database;
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
using Exo.Rp.Core.Tasks;
using Exo.Rp.Core.Tasks.Shutdown.Tasks;
using Exo.Rp.Core.Tasks.Startup.Tasks;
using Exo.Rp.Core.Teams;
using Exo.Rp.Core.Translation;
using Exo.Rp.Core.Updateable;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.World;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Exo.Rp.Core
{
    public class Program
    {
        private IHost _host;

        public Program() 
            => _host = CreateHostBuilder()
                .Build();

        public static IHostBuilder CreateHostBuilder() 
            => Host.CreateDefaultBuilder()
                .ConfigureHostConfiguration((builder) => {
                    builder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddUserSecrets<Core>();
                })
                .ConfigureLogging((context, builder) => {
                    builder
                        .AddConfiguration(context.Configuration)
                        //.ClearProviders()
                        //.AddProvider(null)
                        .AddSentry();
                })
                .ConfigureServices(ConfigureServices)
                .UseConsoleLifetime();

        public async static void ConfigureServices(HostBuilderContext context, IServiceCollection collection) {
            collection
                .AddSingleton<IMapper>(AutoMapperConfiguration.GetMapper())
                //.AddSingleton(typeof(ILogger), typeof(Logger))
                //.AddSingleton(typeof(ILogger<>), typeof(Logger<>))
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

            // Add shutdown tasks
            collection.AddShutdownTask<DisposeManagerTask>();

            // Initialize database
            DatabaseCore databaseCore = new DatabaseCore();
            databaseCore.CreateDatabaseConnection();
            collection.AddSingleton(ContextFactory.Connect());

            // Start loading database models
            await databaseCore.OnResourceStartHandler();

            // Add Core
            collection.AddHostedService<Core>();

            Console.WriteLine(T._("Hello world {0}", player: null, ""));
        }

/*
        public async override void OnStart()
        {
            await _host.StartAsync();
        }

        public async override void OnStop()
        {
            Console.WriteLine("OnStop");
            await _host.StopAsync();
        }
*/
    }
}