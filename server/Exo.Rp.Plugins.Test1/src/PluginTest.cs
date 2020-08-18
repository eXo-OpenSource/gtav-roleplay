using System;
using System.Reflection.Metadata;
using Exo.Rp.Sdk;
using Microsoft.Extensions.DependencyInjection;

namespace plugins.Test1
{
    public class PluginTest : IPlugin
    {
        private readonly IServiceProvider serviceProvider;

        public PluginTest(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Load()
        {
            
        }

        public void Dispose()
        {

        }

        public void Tick()
        {

        }
    }
}