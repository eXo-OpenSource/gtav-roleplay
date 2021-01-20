using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exo.Rp.Core.Admin;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Util;

namespace Exo.Rp.Core.Commands
{
    public class CommandHandler : IManager
    {
        private readonly IList<(CommandAttribute command, MethodInfo method)> _commands;

        public CommandHandler(RuntimeIndexer indexer)
        {
            _commands = indexer.IndexWithAttribute<CommandAttribute, MethodInfo>(Assembly.GetExecutingAssembly(),
                method => method.IsStatic && method.IsPublic).ToList();
        }

        public CommandInvokeResult Invoke(string commandIdentifier, IPlayer player, object[] commandArguments)
        {
            var tuple = _commands.FirstOrDefault(x =>
                x.command.CommandIdentifier.Equals(commandIdentifier) || (x.command.Alias != null && x.command.Alias.Equals(commandIdentifier)));
            if (tuple == default)
                return CommandInvokeResult.NotFound;

            // Check permission
            if (tuple.command.RequiredAdminLevel != default && !player.HasPermission(tuple.command.RequiredAdminLevel, false))
                return CommandInvokeResult.PermissionDenied;

            // push the player to the context
            var args = new object[] {player, commandArguments};

            // Check for greedy arg
            if (tuple.command.GreedyArg && commandArguments is string[])
            {
                args = new object[] {player, string.Join(" ", commandArguments)};
            }

            // Flatten the args (https://stackoverflow.com/questions/21562326/flatten-an-array-of-objects-that-may-contain-arrays)
            var flattenArgs = args.SelectMany(x => x is Array array ? array.Cast<object>() : Enumerable.Repeat(x, 1)).ToArray();

            // Check if the args match up
            if (flattenArgs.Length != tuple.method.GetParameters().Length)
            {
                return CommandInvokeResult.ParameterCountMissmatch;
            }

            // Invoke the handler
            tuple.method.Invoke(null, flattenArgs);
            return CommandInvokeResult.Success;
        }
    }
}