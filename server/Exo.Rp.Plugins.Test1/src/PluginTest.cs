using System;
using System.Reflection.Metadata;
using Exo.Rp.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace plugins.Test1
{
    public class PluginTest : IPlugin
    {
        private readonly ILogger<PluginTest> _logger;

        public PluginTest(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetService<ILogger<PluginTest>>();
        }

        public void Load()
        {

        }

        public void Dispose()
        {

        }

        public void Tick()
        {
            _logger.Debug("Tick() called.");
        }
    }
}