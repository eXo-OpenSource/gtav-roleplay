using System;
using Newtonsoft.Json;
using Sentry;

namespace server.Util.Settings
{
    public class SentrySettings
    {
        [JsonProperty("DSN")] 
        private string DsnUrl;

        [JsonIgnore]
        public Dsn Dsn
        {
            get => new Dsn(DsnUrl);
            set => DsnUrl = value.ToString();
        }

        [JsonProperty("Environment")] 
        public string Environment;

        [JsonProperty("Release")] 
        public string Release;

        [JsonProperty("Debug")] 
        private string Debug;

        [JsonIgnore]
        public bool EnableDebug
        { 
            get => Boolean.Parse(Debug);
            set => Debug = value.ToString();
        }
    }
}