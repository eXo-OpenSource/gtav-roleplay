using System;

namespace server.plugins.core
{
    public interface IPlugin : IDisposable
    {
        public void Load();
        public void Tick();
    }
}