using System;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Util.Log
{
    public class LogTest
    {
        public LogCat Category { get; set; }

        public ConsoleColor Color;

        public string[] Messages;

        public string Message => Category.HasFlag(LogCat.None) ? ""
            : $"{Messages[0]} | {Messages[1]} | {Messages[2]} | {Messages[3]}";
    }
}