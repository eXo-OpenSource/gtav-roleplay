using System.Linq;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Players.Accounts;
using server.Players.Characters;

namespace server.Players
{
    public static class PlayerExtensions
    {
        public static Account GetAccountModel(this IPlayer player)
        {
            return ContextFactory.Instance.AccountModel?.Local.FirstOrDefault(x => x.SocialClubName == player.SocialClubId.ToString());
        }

        public static Account GetAccount(this IPlayer player)
        {
            return PlayerManager.GetAccount(player);
        }

        public static Character GetCharacter(this IPlayer player)
        {
            return PlayerManager.GetCharacter(player);
        }

        public static int GetId(this IPlayer player)
        {
            return player.GetCharacter().Id;
        }

        public static void SendNotification(this IPlayer player, string text)
        {
            player.Emit("sendNotification", text);
        }

        public static void SendInformation(this IPlayer player, string text)
        {
            player.SendNotification("~b~Info~w~: " + text);
        }

        public static void SendWarning(this IPlayer player, string text)
        {
            player.SendNotification("~o~Warnung~w~: " + text);
        }

        public static void SendError(this IPlayer player, string text)
        {
            player.SendNotification("~r~Fehler~w~: " + text);
        }

        public static void SendSuccess(this IPlayer player, string text)
        {
            player.SendNotification("~g~Erfolgreich~w~: " + text);
        }

        public static void StopAnimation(this IPlayer player)
        {

        }
        public static void PlayAnimation(this IPlayer player, string animation, string v, int flag)
        {

        }

        public static void SendChatMessage(this IPlayer player, string msg)
        {

        }

        public static void SetIntoVehicle(this IPlayer player, IVehicle veh, int seat)
        {

        }
    }
}
