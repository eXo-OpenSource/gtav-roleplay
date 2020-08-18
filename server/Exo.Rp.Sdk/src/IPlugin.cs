using System;

namespace Exo.Rp.Sdk
{
    public interface IPlugin : IDisposable
    {
        public void Load();
        public void Tick();
    }
}