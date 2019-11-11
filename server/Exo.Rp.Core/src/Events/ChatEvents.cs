using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AltV.Net;
using server.Commands;
using server.Players;

namespace server.Events
{
    class ChatEvents : IScript
    {
        [Event("Chat:Message")]
        public void ChatMessage(IPlayer player, string msg)
        {
            if (msg[0] == '/')
            {
                var command = msg.TrimStart('/');
                if (command.Length > 0)
                {
                    Alt.Log($"[Chat:CMD] {player.Name}: /{command}");
                    var args = msg.Split(" ");
                    command = args[0].TrimStart('/'); ;
                    args = args.Skip(1).ToArray();
                    Core.GetService<CommandHandler>().Invoke(command, player, args);
                }
            }
            else
            {
                Alt.Log($"[Chat:Message] {player.Name}: {msg}");

            }
        }
    }
}
