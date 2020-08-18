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
        public void ServerOutput(string message, params object[] args);
        public LogMessage GetPrefix(LogCat category, string message, params object[] args);
        public void LogToLogOutput(LogMessage logMessage);
    }

    public interface ILogger<T> : ILogger {}
}