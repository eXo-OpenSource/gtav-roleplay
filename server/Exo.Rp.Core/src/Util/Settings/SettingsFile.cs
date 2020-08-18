using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Enums;
using Newtonsoft.Json;

namespace Exo.Rp.Core.Util.Settings
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