using System;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Sdk.Logger
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

        public void WriteToConsole()
        {
            if (Messages.Length == 4)
            {
                Console.Write(Messages[0]);
                Console.Write(" | ");
                Console.ForegroundColor = Color;
                Console.Write(Messages[1]);
                Console.ResetColor();
                Console.Write(" | ");
                Console.Write(Messages[2]);
                Console.Write(" | ");
                Console.Write(Messages[3]);
                Console.Write("\n");
            }
            else
            {
                Console.WriteLine(Messages[0]);
            }
        }
    }
}