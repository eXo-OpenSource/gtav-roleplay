using Exo.Rp.Core.Translation;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Admin
{
    public static class AdminStatic
    {
        public static bool HasPermission(this IPlayer player, AdminLevel adminLevel, bool notification = true)
        {
            if (player.GetAccount()?.AdminLvl >= adminLevel) return true;

            if (notification) player.SendError(T._("Du bist nicht berechtigt diese Funktion zu nutzen!", player));
            return false;
        }
    }
}