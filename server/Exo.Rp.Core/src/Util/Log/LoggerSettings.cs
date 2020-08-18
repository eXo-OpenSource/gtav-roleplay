using Exo.Rp.Models.Enums;
using Newtonsoft.Json;

namespace Exo.Rp.Core.Util.Log
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