using System;
using models.Enums;

namespace server.Util.Log
{
    public class LogMessage
    {
        public LogCat Category { get; set; }

        public ConsoleColor Color;

        public string[] Messages;

        public string GetMessage()
        {
            return Messages.Length == 4 ? $"{Messages[0]} | {Messages[1]} | {Messages[2]} | {Messages[3]}" : Messages[0];
        }
    }
}
