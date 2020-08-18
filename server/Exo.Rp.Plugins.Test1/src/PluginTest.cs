using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;

namespace plugins.Test1
{
    public class PluginTest : IPlugin
    {
        private readonly ILogger<PluginTest> _logger;

        public PluginTest(ILogger<PluginTest> logger)
        {
            _logger = logger;
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