using Exo.Rp.Sdk.Logger;
using models.Enums;
using Newtonsoft.Json;

namespace server.Util.Log
{
    public class LoggerSettings
    {
        [JsonProperty("LogFileFlags")]
        public LogCat LogFileFlags { get; set; }

        [JsonProperty("PathToLogFolder")]
        public string PathToLogFolder { get; set; }

        [JsonProperty("FileName")]
        public string FileName { get; set; }
    }
}