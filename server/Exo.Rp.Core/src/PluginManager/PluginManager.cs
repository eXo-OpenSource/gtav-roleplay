using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AltV.Net;
using server.plugins.core;
using server.Updateable;
using server.Util;
using Directory = System.IO.Directory;

namespace server.PluginManager
{
    public class PluginManager : IManager, IUpdateable, IDisposable
    {
        private readonly RuntimeIndexer _indexer;
        private readonly List<IPlugin> _plugins;

        public PluginManager(RuntimeIndexer indexer)
        {
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
            var plugins = Directory
                .GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "resources", Alt.Server.Resource.Name, "plugins"), "*.plugin.dll", SearchOption.TopDirectoryOnly)
                .ToList().Select(Alt.LoadAssemblyFromPath);

            foreach (var plugin in plugins)
            {
                _indexer.IndexImplementsInterface<IPlugin>(plugin, type =>
                {
                    _plugins.Add(Activator.CreateInstance(type) as IPlugin);
                });
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