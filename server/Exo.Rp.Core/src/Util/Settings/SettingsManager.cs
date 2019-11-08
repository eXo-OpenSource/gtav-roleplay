using System.Collections.Generic;
using System.IO;
using models.Enums;
using Newtonsoft.Json;
using server.Util.Log;

namespace server.Util.Settings
{
    public static class SettingsManager
    {
        private static readonly Logger Logger = new Logger(typeof(SettingsManager));

        public static readonly List<LogMessage> LogOutput = new List<LogMessage>();

        public static SettingsFile ServerSettings;

        public static bool LoadSettings(string path)
        {
            if (!System.IO.File.Exists(path))
                return false;

            try
            {
                var settings = JsonConvert.DeserializeObject<SettingsFile>(File.ReadAllText(path));
                ServerSettings = settings;

                return true;
            }
            catch (JsonException ex)
            {
                Logger.Error($"Error trying to load the settings file \"{path}\". Error message: {ex.Message}");
                return false;
            }
        }

        public static bool CreateSettings(string settingsPath, string logsPath)
        {
            var folder = Path.GetDirectoryName(settingsPath);

            if (System.IO.File.Exists(settingsPath))
                return false;

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(settingsPath);

            if (!Directory.Exists(logsPath))
                Directory.CreateDirectory(logsPath);

            var defaultSettings = new SettingsFile
            {
                ServerConsoleFlags = LogCat.None | LogCat.Debug | LogCat.Info | LogCat.Warn | LogCat.Error | LogCat.Fatal | LogCat.AltV,
                Database = new DatabaseSettings
                {
                    Server = "148.251.132.111",
                    Database = "gtav_dev",
                    UserId = "exoV",
                    Password = "6LdyVn3uQJbfdSJJ",
                    Port = 6033,
                    QueryLog = false,
                },
                Logger = new LoggerSettings
                {
                    LogFileFlags = LogCat.None | LogCat.Debug | LogCat.Info | LogCat.Warn | LogCat.Error | LogCat.Fatal | LogCat.AltV,
                    PathToLogFolder = logsPath,
                    FileName = "{0}.log"
                },
                DataSync = new DataSyncSettings
                {
                    Interval = 500,
                },
                MetricsCollector = new MetricsCollectorSettings
                {
                    Interval = 10 * 60 * 1000,
                    Host = "https://influxdb.merx.dev:443",
                    Database = "exov",
                    User = "exov",
                    Password = "Cxv.oF73J!i8CtpebZvNFYuRHwW*!GCY"
                }
            };

            ServerSettings = defaultSettings;
            System.IO.File.WriteAllText(settingsPath, JsonConvert.SerializeObject(ServerSettings, Formatting.Indented));

            return true;
        }
    }
}
