using System;
using Newtonsoft.Json;
using Sentry;
using Sentry.Protocol;

namespace server.Util.Settings
{
    public class SentrySettings
    {
        [JsonProperty("DSN")]
        private string _dsnUrl;

        [JsonIgnore]
        public Dsn Dsn
        {
            get => new Dsn(_dsnUrl);
            set => _dsnUrl = value?.ToString();
        }

        [JsonProperty("Environment")]
        public string Environment;

        [JsonProperty("Release")]
        public string Release;

        [JsonProperty("Debug")]
        private string _debug;

        [JsonIgnore]
        public bool Debug
        {
            get => bool.Parse(_debug);
            set => _debug = value.ToString();
        }

        [JsonProperty("LoggerLevel")]
        public SentryLevel LoggerLevel;
    }
}