using Exo.Rp.Sdk.Logger;
using models.Enums;
using Newtonsoft.Json;
using server.Util.Log;

namespace server.Util.Settings
{
    public class SettingsFile
    {
        [JsonProperty("ServerConsoleFlags")]
        public LogCat ServerConsoleFlags { get; set; }

        [JsonProperty("Database")]
        public DatabaseSettings Database { get; set; }

        [JsonProperty("Logger")]
        public LoggerSettings Logger { get; set; }

        [JsonProperty("DataSync")]
        public DataSyncSettings DataSync { get; set; }

        [JsonProperty("MetricsCollector")]
        public MetricsCollectorSettings MetricsCollector { get; set; }

        [JsonProperty("WotlabApi")]
        public WotlabApiSettings WotlabApi { get; set; }

        [JsonProperty("Sentry")]
        public SentrySettings Sentry { get; set; }
    }
}