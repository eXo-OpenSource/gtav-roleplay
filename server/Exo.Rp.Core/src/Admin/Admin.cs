using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Enums;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Admin
{
    internal class Admin : IScript
    {
        private static readonly ILogger<Admin> Logger = new Logger<Admin>();

        private Dictionary<IPlayer, AdminLevel> _admins;

        public Admin()
        {
            _admins = new Dictionary<IPlayer, AdminLevel>();
        }

        private static IEnumerable<IPlayer> GetAdmins()
        {
            var admins = new List<IPlayer>();
            foreach (var player in Alt.GetAllPlayers().Cast<IPlayer>())
                if (player.HasPermission(AdminLevel.TicketSupporter, false))
                    admins.Add(player);

            return admins;
        }

        [ClientEvent("onIPlayerConsoleText")]
        public void OnIPlayerConsoleText(IPlayer player, params object[] arguments)
        {
            Logger.Debug("Console Output from {0}: {1}", player.Name, arguments[0]);
        }

        ////[Command("a", GreedyArg = true)]
        public void AdminChat(IPlayer sourcePlayer, string text)
        {
            if (!sourcePlayer.HasPermission(AdminLevel.TicketSupporter)) return;

            var adminLevel = sourcePlayer.GetAccount().AdminLvl;
            foreach (var player in GetAdmins())
                player.SendChatMessage($"#y#[{adminLevel.ToString()} {sourcePlayer.Name}]#w#: {text}");
        }

        ////[Command("o", GreedyArg = true)]
        public void AdminGlobalChat(IPlayer sourcePlayer, string text)
        {
            if (!sourcePlayer.HasPermission(AdminLevel.TicketSupporter)) return;

            var adminLevel = sourcePlayer.GetAccount().AdminLvl;
            foreach (var player in Alt.GetAllPlayers().Cast<IPlayer>())
                player.SendChatMessage($"#b#[{adminLevel.ToString()} {sourcePlayer.Name}]#w#: {text}");
        }
    }
}