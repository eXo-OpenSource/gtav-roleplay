using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using models.Enums;
using Sentry;
using Sentry.Protocol;
using server.Util.Settings;

namespace server.Util.Log
{
    public class SentryLogger : Logger, Sentry.Extensibility.IDiagnosticLogger
    {
        private readonly SentryLevel _lowestLevel;
        
        public SentryLogger(SentryLevel lowestLevel = SentryLevel.Info) 
            : base(typeof(SentrySdk))
        {
            _lowestLevel = lowestLevel;
        }
        
        public bool IsEnabled(SentryLevel level)
        {
            return _lowestLevel >= level;
        }

        public void Log(SentryLevel logLevel, string message, Exception exception = null, params object[] args)
        {
            if (exception != default)
            {
                Error(exception.Message, args);
                return;
            }
            
            Debug(message, args);
        }
    }
    
    public class Logger<TClass> : Logger
    {

        public Logger()
            : base(typeof(TClass))
        { }
    }
    
    public class Logger 
    {
        private readonly string _parent;

        public Logger(MemberInfo parent)
        {
            _parent = parent.Name;
        }

        public void Debug(string message, params object[] args)
        {
            #if DEBUG
                Log(LogCat.Debug, message, args);
            #endif
        }

        public void Info(string message, params object[] args)
        {
            Log(LogCat.Info, message, args);
        }

        public void Warn(string message, params object[] args)
        {
            Log(LogCat.Warn, message, args);
        }

        public void Error(string message, params object[] args)
        {
            Log(LogCat.Error, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            Log(LogCat.Fatal, message, args);
        }

        public void ServerOutput(string message, params object[] args)
        {
            Log(LogCat.AltV, message, args);
        }

        public LogMessage GetPrefix(LogCat category, string message, params object[] args)
        {
            var date = DateTime.Now;

            var textMessage = (args.Length > 0) ? string.Format(message, args) : message;

            var parent = category.HasFlag(LogCat.AltV) ? "ServerOutput" : _parent;

            var logMsg = new LogMessage
            {
                Color = LogManager.LogCatToColor(category),
                Messages = new[]
                {
                    $@"[{date,12:HH:mm:ss.fff}]",
                    $@"{category.ToString(),6}",
                    $@"{parent}",
                    textMessage
                }
            };

            return logMsg;
        }

        private void Log(LogCat category, string message, params object[] args)
        {
            var flags = SettingsManager.ServerSettings?.ServerConsoleFlags;

            flags ??= LogCat.None | LogCat.Debug | LogCat.Info | LogCat.Warn | LogCat.Error | LogCat.Fatal | LogCat.AltV;

            var logMsg = GetPrefix(category, message, args);

            if (LogManager.IsLogCatIncluded(logMsg.Category, (LogCat)flags))
            {
                logMsg.WriteToConsole();
            }

            SettingsManager.LogOutput.Add(logMsg);
        }

        public void LogToLogOutput(LogMessage logMessage)
        {
            SettingsManager.LogOutput.Add(logMessage);
        }
    }
}
