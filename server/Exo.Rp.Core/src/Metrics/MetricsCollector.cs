using System;
using System.Collections.Generic;
using AltV.Net;
using Exo.Rp.Sdk;
using InfluxDB.Collector;
using server.Util.Log;
using server.Util.Settings;
using Timer = System.Timers.Timer;

namespace server.Metrics
{
    public class MetricsCollector : IDisposable
    {
        private readonly ILogger<MetricsCollector> _logger;
        private readonly InfluxDB.Collector.MetricsCollector _collector;
        private readonly Timer _timer = new Timer();

        public MetricsCollector(ILogger<MetricsCollector> logger)
        {
            _logger = logger;
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

        public void Dispose()
        {
            _timer.Enabled = false;
            _collector.Dispose();
        }

        private void Collect()
        {
            _logger.Info("Writing metrics to InfluxDB...");
            _collector.Write("player_count",
                new Dictionary<string, object>
                {
                    { "value", Alt.GetAllPlayers().Count }
                });
        }
    }
}