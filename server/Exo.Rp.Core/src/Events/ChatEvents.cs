using System;
using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using Exo.Rp.Core.Commands;
using Exo.Rp.Core.Chat;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Translation;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using Sentry;

namespace Exo.Rp.Core.Events
{
    class ChatEvents : IScript
    {
        private static readonly ILogger<ChatEvents> Logger = new Logger<ChatEvents>();

        [ClientEvent("Chat:Hide")]
        public void Hide(IPlayer player, bool state = true)
        {
            player.HideChat(state);
        }

        [ClientEvent("Chat:Message")]
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
                            player.SendError(T._("Befehl wurde nicht gefunden.", player));
                            break;
                        case CommandInvokeResult.PermissionDenied:
                            player.SendError(T._("Du bist nicht berechtigt diese Funktion zu nutzen.", player));
                            break;
                        case CommandInvokeResult.ParameterCountMissmatch:
                            player.SendError(T._("Zuviel oder zu wenig Argumente.", player));
                            break;
                    }
                }
                catch (Exception e)
                {
                    player.SendError(T._("Befehl konnte nicht ausgeführt werden.", player));
                    e.WithScopeOrThrow(s =>
                    {
                        s.User = player.SentryContext;

                        SentrySdk.AddBreadcrumb(null, "Command", null, new Dictionary<string, string> { { "command", command }, { "args", string.Join(' ', args) } });
                        var correlationId = (e.InnerException ?? e).TrackOrThrow();

                        var rootException = e.InnerException ?? e;
                        Logger.Error($"{rootException.Source}: {rootException.Message}\n{rootException.StackTrace}");
                        Logger.Error(
                            $"A runtime exception occured during the execution of command: [{player.ToString()}: {msg}]");

                        player.SendCorrelationId(correlationId);
                    });
                }
            }
            else
            {
                ChatManager.OocChat(player, msg);
                Logger.Debug($"Chat | {player.ToString()}: {msg}");
            }
        }
    }
}