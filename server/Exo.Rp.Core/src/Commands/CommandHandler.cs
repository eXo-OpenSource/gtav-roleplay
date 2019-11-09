using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using server.Admin;
using server.Players;
using server.Util;
using server.Util.Log;

namespace server.Commands
{
    public class CommandHandler : IManager
    {
        private static readonly Logger<CommandHandler> Logger = new Logger<CommandHandler>();

        private readonly Dictionary<string, (Command command, MethodInfo method)> _commands;

        public CommandHandler(MethodIndexer indexer)
        {
            _commands = new Dictionary<string, (Command, MethodInfo)>();
            indexer.IndexWithAttribute<Command, MethodInfo>(Assembly.GetExecutingAssembly(),
                method => method.IsStatic && method.IsPublic,
                pair => _commands.Add(pair.attribute.CommandIdentifier, (pair.attribute, pair.memberInfo)));
        }

        public void Invoke(string commandIdentifier, IPlayer player, params object[] commandArguments)
        {
            if (!_commands.TryGetValue(commandIdentifier, out var tuple)) return;

            // Check permission
            if (tuple.command.RequiredAdminLevel != default && !player.HasPermission(tuple.command.RequiredAdminLevel))
                return;

            // push the player to the context
            var args = new object[] {player, commandArguments};

            // Check for greedy arg
            if (tuple.command.GreedyArg && commandArguments is string[])
            {
                args = new object[] {player, string.Join(" ", commandArguments)};
            }

            // Flatten the args (https://stackoverflow.com/questions/21562326/flatten-an-array-of-objects-that-may-contain-arrays)
            var flattenArgs = args.SelectMany(x => x is Array array ? array.Cast<object>() : Enumerable.Repeat(x, 1)).ToArray();
            tuple.method.Invoke(null, flattenArgs);
        }
    }
}