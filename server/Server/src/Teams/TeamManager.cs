using System.Collections.Generic;
using System.Linq;
using AltV.Net.Elements.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using server.AutoMapper;
using server.Database;
using server.Teams.State;
using server.Util.Log;
using shared.Teams;
using Character = server.Players.Characters.Character;

namespace server.Teams
{
    internal class TeamManager
    {
        private static readonly Logger<TeamManager> Logger = new Logger<TeamManager>();

        public static readonly List<DepartmentModel> TeamDepartments;
        public static readonly List<TeamMemberPermissionModel> TeamMemberPermissions;
        public static readonly List<TeamMember> TeamMembers;
        public static readonly List<Team> Teams;

        //public Dictionary<int, List<TeamMemberModel>> TeamMembers;

        static TeamManager()
        {
            Teams = new List<Team>();
            TeamDepartments = new List<DepartmentModel>();
            TeamMembers = new List<TeamMember>();
            TeamMemberPermissions = new List<TeamMemberPermissionModel>();
            Teams.Add(new Team
            {
                Id = 0,
                Name = " - keine -"
            });

            if (!ContextFactory.Instance.TeamModel.Local.Any()) return;
            foreach (var team in ContextFactory.Instance.TeamModel.Local.ToList())
            {
                switch (team.Id)
                {
                    case 1:
                        AddTeam<Lspd>(team);
                        break;
                    default:
                        AddTeam<Team>(team);
                        break;
                }

                ;
            }

            if (ContextFactory.Instance.TeamMemberPermissionModel.Local.Any())
                TeamMemberPermissions.AddRange(ContextFactory.Instance.TeamMemberPermissionModel.Local);
        }

        private static void AddTeam<T>(Team team) 
            where T: Team
        {
            Teams.Add(AutoMapperConfiguration.GetMapper().Map<T>(team));
        }

        public static T GetTeam<T>(int teamId) 
            where T: Team
        {
            return Teams.Find(x => x.Id == teamId) as T;
        }

        public static DepartmentModel GetTeamDepartment(int departmentId)
        {
            return TeamDepartments.Find(x => x.Id == departmentId);
        }

        public static TeamMember GetTeamMember(int memberId)
        {
            return TeamMembers.Find(x => x.Id == memberId);
        }

        public static List<Team> GetTeamsForPlayer(Character player)
        {
            var teams = new List<Team>();

            foreach (var teamMember in GetTeamMembersForPlayer(player)) teams.Add(GetTeam<Team>(teamMember.Id));

            return teams;
        }

        public static List<DepartmentModel> GetDepartmentsForPlayer(Character player)
        {
            var departments = new List<DepartmentModel>();

            foreach (var teamMember in GetTeamMembersForPlayer(player))
                departments.Add(GetTeamDepartment(teamMember.DepartmentId));

            return departments;
        }

        public static IEnumerable<TeamMember> GetTeamMembersForPlayer(Character player)
        {
            return TeamMembers.FindAll(x => x.CharacterId == player.Id);
        }

        public static List<TeamDto> GetTeamsIPlayer()
        {
            var teams = new List<TeamDto>();

            foreach (var team in Teams)
            {
                var teamForIPlayer = new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    Description = team.Description,
                    Departments = new List<DepartmentDto>()
                };
                
                if (team.Departments != null)
                    foreach (var department in team.Departments)
                    {
                        var departmentForIPlayer = new DepartmentDto
                        {
                            Id = department.Id,
                            Name = department.Name,
                            Description = department.Description,
                            Rank = department.Rank
                        };
                        teamForIPlayer.Departments.Add(departmentForIPlayer);
                    }
                   
                teams.Add(teamForIPlayer);
            }

            return teams;
        }

        public static void SendToIPlayer(IPlayer player)
        {
            player.Emit("Teams:Receive", JsonConvert.SerializeObject(GetTeamsIPlayer()));
        }
    }
}