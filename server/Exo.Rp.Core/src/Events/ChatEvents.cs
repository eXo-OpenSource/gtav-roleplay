using System;
using System.Linq;
using AltV.Net;
using Sentry;
using server.Commands;
using server.Players;
using server.Translation;
using server.Util.Log;

namespace server.Events
{
    class ChatEvents : IScript
    {
        private static readonly Logger<ChatEvents> Logger = new Logger<ChatEvents>();

        [Event("Chat:Message")]
        public void ChatMessage(IPlayer player, string msg)
        {
            if (msg?.Length > 0 && msg[0] == '/')
            {
                var message = msg.Split(' ');
                var command = message[0].Replace("/", string.Empty);
                var args = message.Skip(1).Cast<object>().ToArray();

                if (command.Length <= 0) return;
                Logger.Debug($"Chat | {player.ToString()}: /{command}");

                try
                {
                    switch (Core.GetService<CommandHandler>().Invoke(command, player, args))
                    {
                        case CommandInvokeResult.Success:
                            break;
                        case CommandInvokeResult.NotFound:
                            player.SendError("Befehl wurde nicht gefunden.".Translate(player));
                            break;
                        case CommandInvokeResult.PermissionDenied:
                            player.SendError("Du bist nicht berechtigt diese Funktion zu nutzen.".Translate(player));
                            break;
                    }
                }
                catch (Exception e)
                {
                    SentrySdk.CaptureException(e);

                    var rootException = e.InnerException ?? e;
                    Logger.Error($"{rootException.Source}: {rootException.Message}\n{rootException.StackTrace}");
                    Logger.Error(
                        $"A runtime exception occured during the execution of command: [{player.ToString()}: {msg}]");

                    player.SendError("Befehl konnte nicht ausgeführt werden.".Translate(player));
                }
            }
            else
            {
                Logger.Debug($"Chat | {player.ToString()}: {msg}");
            }
        }
    }
} 