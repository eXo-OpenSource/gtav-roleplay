using AltV.Net.Elements.Entities;
using server.Players;
using Character = server.Players.Characters.Character;
using TeamPermissions = server.Models.Enums.TeamPermissions;

namespace server.Teams
{
    public partial class TeamMember
    {

        public bool HasPermission(IPlayer player, TeamPermissions permission)
        {
            if (!IsInSameTeam(player.GetCharacter())) return false;
            return GetPermissions().HasFlag(permission);
        }

        private bool IsInSameTeam(Character player)
        {
            foreach (var team in TeamManager.GetTeamsForPlayer(player))
                if (team.Id == TeamId)
                    return true;
            return false;
        }

        public TeamPermissions GetPermissions()
        {
            return TeamPermissions.None;
        }
    }
}