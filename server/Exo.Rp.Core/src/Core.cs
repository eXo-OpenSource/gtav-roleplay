using System;
using System.Diagnostics;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Sentry.Protocol;
using server.AutoMapper;
using server.BankAccounts;
using server.Commands;
using server.Database;
using server.Environment;
using server.Factories.BaseObjects;
using server.Factories.Entities;
using server.Inventory;
using server.Inventory.Items;
using server.Jobs;
using server.Players;
using server.Shops;
using server.Streamer;
using server.Streamer.Grid;
using server.Streamer.Private;
using server.Teams;
using server.Translation;
using server.Updateable;
using server.Util;
using server.Util.Log;
using server.Vehicles;
using server.World;
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
				.AddSingleton<PluginManager.PluginManager>()
				.AddSingleton<PrivateStreamer>()
				.AddSingleton<PublicStreamer>()
				.AddSingleton<DoorManager>()
				.AddSingleton<EnvironmentManager>();

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

			Logger.Info("Services | Loading Streamer managers...");
			_serviceProvider.GetService<PrivateStreamer>().Init(1, 500,
				(threadCount, repo) => new BetterServerEventNetworkLayer(threadCount, repo),
				(entity, threadCount) => entity.Type,
				(entityId, entityType, threadCount) => entityType,
				(threadid) => new LimitedPrivateGrid3(50_000, 50_000, 75, 10_000, 10_000,  500)
				, new IdProvider());
			_serviceProvider.GetService<PublicStreamer>();
			AltEntitySync.Init(3, 250,
				(threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository),
				(entity, threadCount) => entity.Type,
				(entityId, entityType, threadCount) => entityType,
				(threadId) =>
				{
					if (threadId == 1)
					{
						return new LimitedGrid3(50_000, 50_000, 125, 10_000, 10_000, 1000);
					}
					/*//THREAD PED
					else if (threadId == 3){
						return new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 64);
					}*/
					else
					{
						return new LimitedGrid3(50_000, 50_000, 175, 10_000, 10_000, 300);
					}
				},
				Core.GetService<PrivateStreamer>().idProvider);
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
			Logger.Info("Services | Loading Door manager...");
			_serviceProvider.GetService<DoorManager>();
			Logger.Info("Services | Loading Environment manager...");
			_serviceProvider.GetService<EnvironmentManager>();
			Logger.Info("Services | Loading Plugin manager...");
			_serviceProvider.GetService<PluginManager.PluginManager>();

			stopWatch.Stop();
			Logger.Debug($"Loaded services in {stopWatch.ElapsedMilliseconds} ms.");

			/*var binsPath = Path.Combine("resources", Alt.Server.Resource.Name, "worldBinsDumpsters.json");
			var bins = JsonConvert.DeserializeObject<List<Bin>>(File.ReadAllText(binsPath));

			foreach (var bin in bins)
			{
				if(!bin.Name.Contains("dumpster")) continue;
				var pos = new Position(bin.Position.X, bin.Position.Y, bin.Position.Z);
				var rot = new Position(bin.Rotation.X, bin.Rotation.Y, bin.Rotation.Z);
				var nModel = new WorldObjects.WorldObject()
				{
					Position = pos.Serialize(),
					Rotation = rot.Serialize(),
					Type = models.Enums.WorldObjects.WasteBin,
					PlacedBy = 1,
					Date = DateTime.Now
				};
				var factory = GetService<DatabaseContext>().WorldObjectsModels.Local;
				factory.Add(nModel);
				Logger.Debug($"{bin.Name}");
			}*/
		}

		public override void OnStop()
		{
			Logger.Info("Disposing Metrics collector...");
			_serviceProvider.GetService<MetricsCollector>().Dispose();
			Logger.Info("Disposing Plugin manager...");
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
