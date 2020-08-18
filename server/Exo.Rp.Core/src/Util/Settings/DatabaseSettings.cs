using Newtonsoft.Json;

namespace Exo.Rp.Core.Util.Settings
{
    public class DatabaseSettings
    {
        [JsonProperty("Server")]
        public string Server { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Database")]
        public string Database { get; set; }

        [JsonProperty("Port")]
        public uint Port { get; set; }

        [JsonProperty("QueryLog")]
        public bool QueryLog { get; set; }
    }
}