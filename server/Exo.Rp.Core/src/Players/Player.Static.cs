using System;
using System.Linq;
using AltV.Net;

namespace server.Players
{
    public partial class Player
    {
        public static IPlayer FindPlayer(IPlayer sender, string name)
        {
            if (name.Contains("_")) name = name.Replace("_", " ");
            IPlayer returnIPlayer = null;
            var players = Alt.GetAllPlayers().Cast<IPlayer>();
            var playersCount = 0;
            foreach (var player in players)
            {
                // Skip if list element is null
                if (player?.GetAccount() == null) continue;

                // If player name contains provided name
                var playerName = player.Name;
                var charName = player.GetCharacter()?.GetNormalizedName();
                if (charName != null && !playerName.ToLower().Contains(name.ToLower()) &&
                    !charName.ToLower().Contains(name.ToLower())) continue;
                // If player name == provided name
                if (charName != null && (playerName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                                         charName.Equals(name, StringComparison.OrdinalIgnoreCase))) return player;

                playersCount++;
                returnIPlayer = player;
            }

            if (playersCount == 1) return returnIPlayer;
            return null;
            /*
            API.Shared.SendChatMessageToPlayer(sender,
                playersCount > 0
                    ? "#r#ERROR: #w#Es wurden mehrere Spieler mit dem angegebenen Namen gefunden."
                    : "#r#ERROR: #w#Spieler wurde nicht gefunden.");
            return null;
            */
        }

        public static IPlayer GetFromId(int id)
        {
            foreach (var player in AltV.Net.Alt.GetAllPlayers().Cast<IPlayer>())
            {
                if (player.Id != id) continue;
                return player;
            }

            return null;
        }
    }
}
