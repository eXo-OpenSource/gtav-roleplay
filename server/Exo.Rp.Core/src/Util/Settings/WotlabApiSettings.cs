using Newtonsoft.Json;

namespace server.Util.Settings
{
    public class WotlabApiSettings
    {
        [JsonProperty("Url")] 
        public string Url { get; set; }

        [JsonProperty("Secret")] 
        public string Secret { get; set; }

        [JsonProperty("OnlyBeta")] 
        public bool OnlyBeta { get; set; }
        
        [JsonProperty("BetaGroupId")]
        public int BetaGroupId { get; set; }
    }
}