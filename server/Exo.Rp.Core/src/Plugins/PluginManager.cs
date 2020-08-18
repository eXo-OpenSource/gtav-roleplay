using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AltV.Net;
using Exo.Rp.Core.Updateable;
using Exo.Rp.Core.Util;
using Exo.Rp.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Directory = System.IO.Directory;

namespace Exo.Rp.Core.Plugins
{
    public class PluginManager : IManager, IUpdateable, IDisposable
    {
        private static readonly string PluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "resources", Alt.Server.Resource.Name,
            "plugins");

        private readonly RuntimeIndexer _indexer;
        private readonly List<IPlugin> _plugins;
        private readonly IServiceProvider _serviceProvider;

        public PluginManager(IServiceProvider serviceProvider, RuntimeIndexer indexer)
        {
            _serviceProvider = serviceProvider;
            _indexer = indexer;
            _plugins = new List<IPlugin>();
            IndexPlugins();
            LoadPlugins();
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
                .ToList()
                .Select(Alt.LoadAssemblyFromPath);

            var result = _indexer.IndexImplementsInterface<IPlugin>(plugins)
                .Select(type => ActivatorUtilities.CreateInstance(_serviceProvider, type) as IPlugin);
            foreach (var plugin in result)
            {
                _plugins.Add(plugin);
            }
        }

        private void LoadPlugins()
        {
            _plugins.ForEach(plugin => plugin.Load());
        }

        public void Tick()
        {
            _plugins.ForEach(plugin => plugin.Tick());
        }
    }
}