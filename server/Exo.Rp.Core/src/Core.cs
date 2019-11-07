using System;
using System.Diagnostics;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using server.AutoMapper;
using server.BankAccounts;
using server.Database;
using server.Environment;
using server.Extensions;
using server.Factories.Entity;
using server.Inventory;
using server.Inventory.Items;
using server.Jobs;
using server.Players;
using server.Shops;
using server.Teams;
using server.Util.Extensions;
using server.Util.Log;
using server.Vehicles;
using MetricsCollector = server.Metrics.MetricsCollector;
using IPlayer = AltV.Net.Elements.Entities.IPlayer;
using IVehicle = AltV.Net.Elements.Entities.IVehicle;

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

            // Prepare service provider
            var serviceCollection = new ServiceCollection()
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
                .AddSingleton<JobManager>();

            // Start loading database models
            _databaseCore.OnResourceStartHandler(() =>
            {
                _serviceProvider = serviceCollection
                    .AddSingleton(ContextFactory.Instance)
                    .BuildServiceProvider();
                LoadServices();
            });
        }

        private static void LoadServices()
        {
            Logger.Info("Loading services...");
            var stopWatch = Stopwatch.StartNew();

            Logger.Info("Services | Loading Metrics collector...");
            _serviceProvider.GetService<MetricsCollector>().Start();
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

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new PlayerEntityFactory();
        }

        /*
        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new VehicleEntityFactory();
        }
        */

        public static T GetService<T>()
            where T : IService
        {
            return _serviceProvider.GetService<T>();
        }
    }
}