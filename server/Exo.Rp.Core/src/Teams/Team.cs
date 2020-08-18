using System.Collections.Generic;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Teams;
using Exo.Rp.Sdk;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Teams
{
    public partial class Team
    {
        private static readonly ILogger<Team> Logger = new Logger<Team>();

        public void AddPlayer(IPlayer player, int departmentId, int rank)
        {
            var teamMemberModel = new TeamMember
            {
                TeamId = Id,
                DepartmentId = departmentId,
                Rank = rank,
                CharacterId = player.GetCharacter().Id
            };

            Core.GetService<DatabaseContext>().TeamMemberModel.Local.Add(teamMemberModel);
            //DatabaseCore.SaveChangeToDatabase();
            //TODO player inform etc

            // TODO: Why was try catch used here?
            /*
            using (var db = new DatabaseContext())
            {
                try
                {
                    db.TeamMemberModel.Add(teamMemberModel);
                    db.SaveChanges();
                    //TODO player inform etc
                }
                catch
                {
                    //
                }
            }
            */
        }

        public bool KickPlayer(IPlayer source, int targetId)
        {
            /*
            using (var db = new DatabaseContext())
            {
                try
                {
                    // Core.GetService<DatabaseContext>().TeamMemberModel.Local.Remove(members); ?
                    //db.TeamMemberModel.Remove(members);
                    db.SaveChanges();
                    //
                }
                catch
                {
                    //
                }
            }
            */
            return false;
        }

        public DepartmentModel GetDepartmentForPlayer(TeamMember teamMember)
        {
            return Core.GetService<TeamManager>().TeamDepartments.Find(x => x.Id == teamMember.DepartmentId);
        }

        public List<TeamMemberDto> GetTeamMembersForIPlayer()
        {
            var members = new List<TeamMemberDto>();

            foreach (var member in TeamMembers)
                members.Add(new TeamMemberDto
                {
                    Id = member.Id,
                    AccountId = member.CharacterId,
                    DepartmentId = member.DepartmentId,
                    Rank = member.Rank
                });

            return members;
        }

        public int GetMoney()
        {
            return BankAccount.Money;
        }

        public void GiveMoney(int amount, string reason, bool silent)
        {
            BankAccount.Money += amount;
        }

        public void TakeMoney(int amount, string reason, bool silent)
        {
            BankAccount.Money -= amount;
        }
    }
}