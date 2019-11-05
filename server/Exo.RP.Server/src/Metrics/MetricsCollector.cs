using System;
using System.Collections.Generic;
using System.Timers;
using AltV.Net;
using InfluxDB.Collector;
using server.Util.Log;
using server.Util.Settings;

namespace server.Metrics
{
    public class MetricsCollector
    {
        private static readonly Logger<MetricsCollector> Logger = new Logger<MetricsCollector>();

        private readonly InfluxDB.Collector.MetricsCollector _collector;
        private readonly Timer _timer = new Timer();

        public MetricsCollector()
        {
            var settings = SettingsManager.ServerSettings.MetricsCollector;
            _collector = new CollectorConfiguration()
                .Batch.AtInterval(TimeSpan.FromSeconds(2))
                .WriteTo.InfluxDB(settings.Host, settings.Database, settings.User, settings.Password)
                .CreateCollector();
        }

        public void Start()
        {
            _timer.Elapsed += (source, e) => Collect();
            _timer.Interval = SettingsManager.ServerSettings.MetricsCollector.Interval;
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }

        private void Collect()
        {
            Logger.Info("Writing metrics to InfluxDB...");
            _collector.Write("player_count", 
                new Dictionary<string, object>
                {
                    { "value", Alt.GetAllPlayers().Count }
                });
        }
    }
}