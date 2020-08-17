using AltV.Net;
using server.Players;
using server.Teams;
using server.Util.Log;

namespace server.Events
{
	internal class TeamEvents : IScript
	{
		private static readonly Logger<TeamEvents> Logger = new Logger<TeamEvents>();

		//[ServerEvent(Event.ResourceStart)]
		public void Load()
		{

		}

		[Event("Team:toggleDuty")]
		public void ToggleDuty(IPlayer player, params object[] arguments)
		{
			var faction = player.GetCharacter().GetTeams();
			if (faction != null)
			{
				//if (!faction.ToggleDuty(player)) player.SendError("Du bist in keiner Staatsfraktion!");
			}
			else
			{
				player.SendError("Du bist in keiner Fraktion!");
			}
		}

		[Event("Team:kickMember")]
		public void KickMember(IPlayer player, params object[] arguments)
		{
			if (arguments.Length < 2)
			{
				Logger.Debug("Error: Invalid Arguments at Team:kickMember");
				return;
			}

			var teamId = (int) arguments[0];
			var memberId = (int) arguments[1];


			var team = Core.GetService<TeamManager>().GetTeam<Team>(teamId);
			if (team != null)
			{
				if (!team.KickPlayer(player, memberId)) player.SendError("Der Spieler konnte nicht gekickt werden!");
			}
			else
			{
				player.SendError("Du bist in keiner Fraktion!");
			}
		}

		[Event("Team:requestTeamMembers")]
		public void RequestTeamMembers(IPlayer player, int teamId)
		{
			//TODO FIX
			//var members = TeamManager.Instance.GetTeam(teamId).GetTeamMembersForClient();
			//player.Emit("Team:getTeamMembers", JsonConvert.SerializeObject(members));
		}
	}
}
