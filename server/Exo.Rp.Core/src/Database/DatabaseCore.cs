using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AltV.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using models.Enums;
using MySql.Data.MySqlClient;
using Sentry;
using server.Util.Log;
using server.Util.Settings;

namespace server.Database
{
    public class DatabaseCore
    {
        private static readonly Util.Log.Logger<DatabaseCore> Logger = new Util.Log.Logger<DatabaseCore>();

        private IDisposable _sentry;
        private Stopwatch _lastUpdate;

        public void OnResourceStartHandler(Action<SentrySettings, SentryOptions>? configureSentry, Action? onDatabaseInitialized)
        {
            SettingsManager.LogOutput.Add(new LogMessage
            {
                Category = LogCat.None,
                Messages = new[] { "" }
            });
            SettingsManager.LogOutput.Add(new LogMessage
            {
                Category = LogCat.None,
                Messages = new []{ "=================================================" }
            });
            SettingsManager.LogOutput.Add(new LogMessage
            {
                Category = LogCat.None,
                Messages = new[] { "=== Start of server log after server re/start ===" }
            });
            SettingsManager.LogOutput.Add(new LogMessage
            {
                Category = LogCat.None,
                Messages = new[] { "=================================================" }
            });

            /*
            var cls = new SerializationTest("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");

            var timer = Stopwatch.StartNew();
            var str = cls.Serialize();
            timer.Stop();

            var timer2 = Stopwatch.StartNew();
            var str2 = JsonConvert.SerializeObject(cls);
            timer2.Stop();

            Console.WriteLine($"Serialization byte array: {timer.ElapsedTicks} Ticks | json: {timer2.ElapsedTicks} Ticks");
            Console.WriteLine($"byte array length: {str.Length} | json: {str2.Length}");

            timer.Restart();
            var bcls = SerializationTest.Deserialize(str);
            timer.Stop();

            timer2.Restart();
            var jcls = JsonConvert.DeserializeObject<SerializationTest>(str2);
            timer2.Stop();

            Console.WriteLine($"Deserialization time byte array: {timer.ElapsedTicks} Ticks | json: {timer2.ElapsedTicks} Ticks");
            Console.WriteLine($"Org  data number: {cls.number} msg right: {cls.message == cls.message}");
            Console.WriteLine($"Byte data number: {bcls.number} msg right: {bcls.message == cls.message}");
            Console.WriteLine($"Json data number: {jcls.number} msg right: {jcls.message == cls.message}");
            */

            Logger.Info("====== Server started ======");

            var settingsPath = Path.Combine("resources", Alt.Server.Resource.Name, "config.json");
            var logsPath = Path.Combine("resources", Alt.Server.Resource.Name, "logs");

            if (!SetDatabaseConnection(settingsPath, logsPath))
                return;

            // Initialize sentry
            if (SettingsManager.ServerSettings.Sentry.Release.Length > 0)
                _sentry = SentrySdk.Init(sentryOptions => configureSentry(SettingsManager.ServerSettings.Sentry, sentryOptions));

            if (!CreateDatabaseConnection())
                return;

            Logger.Info($"Database connection to server: {SettingsManager.ServerSettings.Database.Server}");

            var stopWatch = Stopwatch.StartNew();

            ContextFactory.Instance.AccountModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.AccountModel.Local.Count} accounts.");

			ContextFactory.Instance.CharacterModel.Load();
			Logger.Info($"Loaded {ContextFactory.Instance.CharacterModel.Local.Count} characters.");

            ContextFactory.Instance.FaceFeaturesModel.Load();

            ContextFactory.Instance.VehicleModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.VehicleModel.Local.Count} vehicles.");

            ContextFactory.Instance.TeamModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.TeamModel.Local.Count} teams.");

            ContextFactory.Instance.TeamDepartmentModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.TeamDepartmentModel.Local.Count} departments.");

            ContextFactory.Instance.TeamMemberModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.TeamMemberModel.Local.Count} team members.");

            ContextFactory.Instance.TeamMemberPermissionModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.TeamMemberPermissionModel.Local.Count} team member permissions.");

            ContextFactory.Instance.BankAccountModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.BankAccountModel.Local.Count} bank accounts.");

            ContextFactory.Instance.ShopModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.ShopModel.Local.Count} shops.");

            ContextFactory.Instance.VehicleShopVehicleModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.VehicleShopVehicleModel.Local.Count} shop vehicles.");

            ContextFactory.Instance.PedModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.PedModel.Local.Count} peds.");

            ContextFactory.Instance.InventoryModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.InventoryModel.Local.Count} inventories.");

            ContextFactory.Instance.ItemModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.ItemModel.Local.Count} items.");

            ContextFactory.Instance.InventoryItemsModel.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.InventoryItemsModel.Local.Count} inventory items.");

            ContextFactory.Instance.WorldObjectsModels.Load();
            Logger.Info($"Loaded {ContextFactory.Instance.WorldObjectsModels.Local.Count} world objects.");

            stopWatch.Stop();

            Logger.Debug($"Loaded database in {stopWatch.ElapsedMilliseconds} ms.");

            _lastUpdate = Stopwatch.StartNew();
            onDatabaseInitialized?.Invoke();
        }

        public void OnResourceStopHandler()
        {
	        if (SettingsManager.ServerSettings.Sentry.Release.Length > 0)
				_sentry.Dispose();
        }

        public static bool SetDatabaseConnection(string settingsPath, string logsPath)
        {
            if (!SettingsManager.LoadSettings(settingsPath))
            {
                Logger.Error($"Unable to load settings from \"{settingsPath}\". Trying to create a new one.");
                if (!SettingsManager.CreateSettings(settingsPath, logsPath))
                {
                    Logger.Fatal("Unable to create a new settings file.");
                    return false;
                }
            }
            else
                Logger.Debug($"Successfully loaded settings from \"{settingsPath}\".");

            return true;
        }

        public static bool CreateDatabaseConnection()
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = SettingsManager.ServerSettings.Database.Server,
                UserID = SettingsManager.ServerSettings.Database.UserId,
                Password = SettingsManager.ServerSettings.Database.Password,
                Database = SettingsManager.ServerSettings.Database.Database,
                Port = SettingsManager.ServerSettings.Database.Port,
                TreatTinyAsBoolean = false,
            };

            ContextFactory.SetConnectionString(connectionStringBuilder);


            return ContextFactory.Instance != null;
        }

        [Event("Update")]
        public async Task OnUpdateHandler()
        {
            if (ContextFactory.Instance == null)
                return;

            if (_lastUpdate.Elapsed.Seconds < 20) return; // .TotalMinutes >= 5

            _lastUpdate.Stop();
            _lastUpdate.Reset();

            await SaveChangeToDatabaseAsync();
            await LogManager.SaveLogsToFileAsync();
            _lastUpdate.Start();
        }

        public static async Task SaveChangeToDatabaseAsync()
        {
            var results = await ContextFactory.Instance.SaveChangesAsync();
            if (results > 0)
                Logger.Debug($"{results} changes saved to the database...");
        }

        public static void SaveChangeToDatabase()
        {
            var results = ContextFactory.Instance.SaveChanges();
            Logger.Debug($"{results} changes saved to the database.");
        }

        public static ILoggerFactory GetLoggerFactory(LogLevel logLevel)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                builder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name,
                        logLevel));
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

    }
}
