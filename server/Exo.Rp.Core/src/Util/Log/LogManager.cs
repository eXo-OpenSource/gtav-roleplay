using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using models.Enums;
using server.Util.Settings;

namespace server.Util.Log
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public static class LogManager
    {
        private static readonly Logger Logger = new Logger(typeof(LogManager));

        public static ConsoleColor LogCatToColor(LogCat category)
        {
            switch (category)
            {
                case LogCat.Info:
                    return ConsoleColor.Green;
                case LogCat.Warn:
                    return ConsoleColor.Yellow;
                case LogCat.Error:
                    return ConsoleColor.Magenta;
                case LogCat.Debug:
                    return ConsoleColor.DarkCyan;
                case LogCat.Fatal:
                    return ConsoleColor.Red;
                case LogCat.None:
                    return ConsoleColor.Gray;
                case LogCat.AltV:
                    return ConsoleColor.DarkCyan;
                default:
                    return ConsoleColor.Gray;
            }

        }

        public static async Task SaveLogsToFileAsync()
        {
            if (SettingsManager.LogOutput.Count <= 1)
                return;

            var date = DateTime.Now;

            var path = SettingsManager.ServerSettings.Logger.PathToLogFolder;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = string.Format(SettingsManager.ServerSettings.Logger.FileName, date.ToString("yyyy-MM-dd"));

            var filePath = Path.Combine(path, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                var file = System.IO.File.Create(filePath);
                file.Close();
                System.IO.File.AppendAllText(filePath, $"====== Start of log file {date:dd/MM/yyyy} ======{System.Environment.NewLine}");
            }

            if (IsFileLockedOrUsed(filePath))
                return;

            var localLogDump = SettingsManager.LogOutput
                .Where(x => IsLogCatIncluded(x.Category, SettingsManager.ServerSettings.Logger.LogFileFlags))
                .Select(x => x.GetMessage()).ToList();

            var lines = localLogDump.Count();
            await System.IO.File.AppendAllLinesAsync(filePath, localLogDump);
            SettingsManager.LogOutput.RemoveRange(0, lines);
            Logger.Debug($"Wrote {lines} lines to log file {fileName}");
        }

        public static bool IsLogCatIncluded(LogCat category, LogCat flags)
        {
            if (flags.HasFlag(LogCat.Debug) && category == LogCat.Debug)
                return true;
            if (flags.HasFlag(LogCat.Info) && category == LogCat.Info)
                return true;
            if (flags.HasFlag(LogCat.Warn) && category == LogCat.Warn)
                return true;
            if (flags.HasFlag(LogCat.Error) && category == LogCat.Error)
                return true;
            if (flags.HasFlag(LogCat.Fatal) && category == LogCat.Fatal)
                return true;
            if (flags.HasFlag(LogCat.AltV) && category == LogCat.AltV)
                return true;

            return category == LogCat.None;
        }

        private static bool IsFileLockedOrUsed(string filePath)
        {
            FileStream stream;

            try
            {
                stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            stream?.Close();

            return false;
        }
    }

}
