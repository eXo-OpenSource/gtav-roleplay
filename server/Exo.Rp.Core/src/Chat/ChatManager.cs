using System.Linq;
using System.Text.RegularExpressions;
using AltV.Net;
using IPlayer = server.Players.IPlayer;

namespace server.Chat
{
    internal class ChatManager : IScript
    {
        private const float _maxChatDistance = 500.0f;
        private const float _maxScreamDistance = 500.0f;
        private const float _maxWhisperDistance = 500.0f;

        //[ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(IPlayer sender, string message)
        {
            message = EscapeMessage(message);
            var senderName = sender.GetCharacter()?.GetNormalizedName();

            foreach (var player in Alt.GetAllPlayers().Cast<IPlayer>())
            {
                if (player == null) continue;
                if (player.Position.Distance(sender.Position) < _maxChatDistance) continue;

                message = $"{senderName ?? $"#r#[[#w#{sender.Name}#r#]]#w#"} sagt: {message}";
                player.SendChatMessage(message);
                player.Emit("outputClientConsole", message, false);
            }
        }

        private void OocChat(IPlayer sender, string message)
        {
            message = EscapeMessage(message);
            var senderName = sender.GetCharacter()?.GetNormalizedName();

            foreach (var player in Alt.GetAllPlayers().Cast<IPlayer>())
            {
                if (player == null) continue;
                if (player.Position.Distance(sender.Position) < _maxChatDistance) continue;

                message = $"#y#OOC#w# {senderName ?? $"#r#[[#w#{sender.Name}#r#]]#w#"}: {message}";
                player.SendChatMessage(message);
                player.Emit("outputClientConsole", message, false);
            }
        }

        private void ScreamChat(IPlayer sender, string message)
        {
            message = EscapeMessage(message);
            var senderName = sender.GetCharacter()?.GetNormalizedName();

            foreach (var player in Alt.GetAllPlayers().Cast<IPlayer>())
            {
                if (player == null) continue;
                if (player.Position.Distance(sender.Position) < _maxScreamDistance) continue;

                message = $"{senderName ?? $"#r#[[#w#{sender.Name}#r#]]#w#"} schreit: {message}";
                player.SendChatMessage(message);
                player.Emit("outputClientConsole", message, false);
            }
        }

        private void WhisperChat(IPlayer sender, string message)
        {
            message = EscapeMessage(message);
            var senderName = sender.GetCharacter()?.GetNormalizedName();

            foreach (var player in Alt.GetAllPlayers().Cast<IPlayer>())
            {
                if (player == null) continue;
                if (player.Position.Distance(sender.Position) < _maxWhisperDistance) continue;

                message = $"{senderName ?? $"#r#[[#w#{sender.Name}#r#]]#w#"} flüstert: {message}";
                player.SendChatMessage(message);
                player.Emit("outputClientConsole", message, false);
            }
        }

        private static string EscapeMessage(string message)
        {
            return Regex.Replace(message, @"(#[a-zA-Z]#)+", string.Empty);
        }
    }
}