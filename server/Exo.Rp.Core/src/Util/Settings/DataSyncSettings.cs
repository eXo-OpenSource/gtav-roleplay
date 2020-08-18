using Newtonsoft.Json;

namespace Exo.Rp.Core.Util.Settings
{
    public class DataSyncSettings
    {
        [JsonProperty("Interval")]
        public double Interval { get; set; }
    }
}