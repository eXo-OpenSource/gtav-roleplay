using Newtonsoft.Json;

namespace Exo.Rp.Core.Util.Settings
{
    public class MetricsCollectorSettings
    {
        [JsonProperty("Interval")]
        public double Interval { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("database")]
        public string Database { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}