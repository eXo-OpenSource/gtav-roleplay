using System;
using models.Enums;

namespace server.Commands
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public string CommandIdentifier { get; }
        public string Alias { get; set; }
        public AdminLevel RequiredAdminLevel { get; set; }
        public TeamPermissions RequiredTeamPermissions { get; set; }
        public bool GreedyArg { get; set; }

        public CommandAttribute(string command)
        {
            CommandIdentifier = command;
        }
    }
}