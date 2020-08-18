using Exo.Rp.Models.Enums;
using Exo.Rp.Sdk.Logger;

namespace Exo.Rp.Sdk
{
    public interface ILogger
    {
        public void Debug(string message, params object[] args);
        public void Info(string message, params object[] args);
        public void Warn(string message, params object[] args);
        public void Error(string message, params object[] args);
        public void Fatal(string message, params object[] args);
    }

    public interface ILogger<T> : ILogger {}
}