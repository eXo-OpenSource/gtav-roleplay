using System;
using models.Enums;

namespace server.Commands
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Command : Attribute
    {
        public string CommandIdentifier { get; }
        public Func<object> Context { get; set; }
        public AdminLevel RequiredAdminLevel { get; set; }
        public TeamPermissions RequiredTeamPermissions { get; set; }
        public bool GreedyArg { get; set; }

        public Command(string command)
        {
            CommandIdentifier = command;
        }
    }
}