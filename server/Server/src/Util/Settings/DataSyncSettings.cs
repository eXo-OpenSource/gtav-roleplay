using Newtonsoft.Json;

namespace server.Util.Settings
{
    public class DataSyncSettings
    {
        [JsonProperty("Interval")]
        public double Interval { get; set; }
    }
}
