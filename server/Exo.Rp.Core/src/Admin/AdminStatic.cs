using models.Enums;
using server.Translation;
using IPlayer = server.Players.IPlayer;

namespace server.Admin
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
