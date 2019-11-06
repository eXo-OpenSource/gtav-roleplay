using System;
using System.Diagnostics;
using AltV.Net;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using server.AutoMapper;
using server.BankAccounts;
using server.Database;
using server.Environment;
using server.Inventory;
using server.Inventory.Items;
using server.Jobs;
using server.Players;
using server.Shops;
using server.Teams;
using server.Util.Log;
using server.Vehicles;
using MetricsCollector = server.Metrics.MetricsCollector;

namespace server
{ 
    public interface IService { }

    public interface IManager : IService { }

    public class Core : Resource
    {
        private static readonly Logger<Core> Logger = new Logger<Core>();

        private DatabaseCore _databaseCore;
        private static IServiceProvider _serviceProvider;

        public override void OnStart()
        {
            /*
            foreach (var player in Alt.GetAllPlayers())
            {
                player.SendError("Server is booting....");
                player.Kick("Server is booting... Please try again later.");
            }
            */

            // Initialize database
            _databaseCore = new DatabaseCore();
            DatabaseCore.OnDatabaseInitialized += LoadServices;

            // Prepare service provider
            try
            {
                _serviceProvider = new ServiceCollection()
                    .AddSingleton<IMapper>(AutoMapperConfiguration.GetMapper())
                    .AddSingleton<MetricsCollector>()
                    .AddSingleton<PlayerManager>()
                    .AddSingleton<TeamManager>()
                    .AddSingleton<VehicleManager>()
                    .AddSingleton<BankAccountManager>()
                    .AddSingleton<ShopManager>()
                    .AddSingleton<ItemManager>()
                    .AddSingleton<InventoryManager>()
                    .AddSingleton<IplManager>()
                    .AddSingleton<JobManager>()
                    .BuildServiceProvider();
            }
            catch (Exception e)
            {
                Logger.Fatal(e.Message);
            }

            // Start loading database models
            _databaseCore.OnResourceStartHandler();
        }

        private static void LoadServices()
        {
            Logger.Info("Loading services...");
            var stopWatch = Stopwatch.StartNew();

            Logger.Info("Services | Loading metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Start();;
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

            stopWatch.Stop();
            Logger.Debug($"Loaded services in {stopWatch.ElapsedMilliseconds} ms.");
        }
        
        public override void OnStop()
        {
            Logger.Info("Stopping metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Stop();
        }

        public static T GetService<T>()
            where T : IService
        {
            return _serviceProvider.GetService<T>();
        }
    }
}