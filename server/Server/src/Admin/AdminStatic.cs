using AltV.Net.Elements.Entities;
using server.Enums;
using server.Players;

namespace server.Admin
{
    public static class AdminStatic
    {
        public static bool HasPermission(this IPlayer player, AdminLevel adminLevel, bool notification = true)
        {
            if (player.GetAccount()?.AdminLvl >= adminLevel) return true;

            if (notification) player.SendError("Du bist nicht berechtigt diese Funktion zu nutzen!");
            return false;
        }
    }
}
