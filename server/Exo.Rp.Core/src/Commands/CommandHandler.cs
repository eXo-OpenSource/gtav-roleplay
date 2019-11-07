using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using server.Admin;
using server.Players;
using server.Util.Log;

namespace server.Commands
{
    public class CommandHandler : IManager
    {
        private static readonly Logger<CommandHandler> Logger = new Logger<CommandHandler>();

        private readonly Dictionary<string, (Command command, MethodInfo method)> _commands;

        public CommandHandler()
        {
            _commands = new Dictionary<string, (Command, MethodInfo)>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var methods in types
                .Where(x => typeof(IHasCommands).IsAssignableFrom(x))
                .Select(t => t.GetMethods()))
            {
                foreach (var method in methods.Where(m => m.IsPublic && m.IsStatic))
                {
                    foreach (var attribute in method.GetCustomAttributes())
                    {
                        if (!(attribute is Command command)) continue;
                        _commands.Add(command.CommandIdentifier, (command, method));
                    }
                }
            }
        }
        
        public void Invoke(string commandIdentifier, IPlayer player, params object[] commandArguments)
        {
            if (!_commands.TryGetValue(commandIdentifier, out var tuple)) return;

            // Check permission
            if (tuple.command.MinAdminLevel != default && !player.HasPermission(tuple.command.MinAdminLevel))
                return;
            
            // Check for greedy arg
            object[] args = { player, commandArguments };
            if (tuple.command.GreedyArg && commandArguments is string[])
            {
                args = new object[] {player, string.Join(" ", commandArguments)};
            }

            tuple.method.Invoke(tuple.command.Context?.Invoke(), args);
        }
    }
}