using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using server.Enums;
using server.Players;

namespace server.Commands
{
    public class PlayerCommands : IScript
    {

        #region Variables

        public const float Pi = (float)Math.PI;

        #endregion


        public static bool CheckAdminPermission(IPlayer player, AdminLevel adminLevel)
        {
            player.GetMetaData("account.adminLevel", out AdminLevel level);

            if (level >= adminLevel) return true;
            player.SendError("Du bist nicht berechtigt diese Funktion zu nutzen!");
            return false;
        }

        //[Command("team")]
        public void Team(IPlayer player, int teamId, int rank)
        {
            /*
            var teamManager = TeamManager.Instance;
            if (teamManager.Teams.TryGetValue(teamId, out var team))
            {
                team.AddPlayer(player, rank);
                player.SendInformation(string.Format("Du wurdest in das Team ~r~{0} ~w~gesetzt! (~b~Rang: {1}]~w~)",
                    team.Data.name, rank));
            }
            else
            {
                player.SendError("Team nicht gefunden!");
            }
            */
            //TODO GET TEAMS COMMAND
        }

        //[Command("getTeams")]
        public void GetTeams(IPlayer player)
        {
            if (player.GetCharacter() == null) return;

            /*
            var character = player.GetCharacter();
            var team = character.Data.Faction;
            if (team != null)
            {
                var rank = character.Data.FactionRank;
                player.SendInformation(string.Format("Du bist in der Fraktion ~r~{0}! ~w~(~b~Rang: {1} ~w~)",
                    team.name, rank != null ? rank.ToString() : "UNBEKANNT"));
            }
            else
            {
                player.SendError("Du bist in keiner Fraktion");
            }
            */
            //TODO GET TEAMS COMMAND
        }

        //[Command("testNotification")]
        public void TestNotification(IPlayer player)
        {
            player.SendInformation("Dies ist eine information!");
            player.SendWarning("Dies ist eine Warnung!");
            player.SendError("Dies ist ein Error!");
        }
    }
}