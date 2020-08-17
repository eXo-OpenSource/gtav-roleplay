using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AltV.Net;
using Exo.Rp.Sdk;
using Microsoft.Extensions.DependencyInjection;
using server.Players;
using server.Updateable;
using server.Util;
using Directory = System.IO.Directory;

namespace server.PluginManager
{
    public class PluginManager : IManager, IUpdateable, IDisposable
    {
        private static readonly string PluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "resources", Alt.Server.Resource.Name,
            "plugins");

        private readonly RuntimeIndexer _indexer;
        private readonly List<IPlugin> _plugins;

        public PluginManager(IServiceProvider serviceProvider, RuntimeIndexer indexer)
        {
            _indexer = indexer;
            _plugins = new List<IPlugin>();
            IndexPlugins();
            LoadPlugins(serviceProvider);
        }

        public void Dispose()
        {
            _plugins.ForEach(plugin => plugin.Dispose());
        }

        private void IndexPlugins()
        {
            if (!Directory.Exists(PluginsPath))
                return;

            var plugins = Directory
                .GetFiles(PluginsPath, "*.plugin.dll", SearchOption.TopDirectoryOnly)
                .ToList().Select(Alt.LoadAssemblyFromPath);

            foreach (var assembly in plugins)
            {
                var result = _indexer.IndexImplementsInterface<IPlugin>(assembly)
                    .Select(t => Activator.CreateInstance(t) as IPlugin);
                foreach (var plugin in result)
                {
                    _plugins.Add(plugin);
                }
            }
        }

        private void LoadPlugins(IServiceProvider serviceProvider)
        {
            _plugins.ForEach(plugin => plugin.Load(serviceProvider));
        }

        public void Tick()
        {
            _plugins.ForEach(plugin => plugin.Tick());
        }
    }
}