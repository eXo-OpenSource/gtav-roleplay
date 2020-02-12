using System.Collections.Generic;
using System.Linq;
using AltV.Net.Elements.Entities;
using AutoMapper;
using models.Teams;
using Newtonsoft.Json;
using server.Database;
using server.Teams.State;
using server.Util.Log;
using Character = server.Players.Characters.Character;

namespace server.Teams
{
    internal class TeamManager : IManager
    {
        private static readonly Logger<TeamManager> Logger = new Logger<TeamManager>();

        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public readonly List<DepartmentModel> TeamDepartments;
        public readonly List<TeamMemberPermissionModel> TeamMemberPermissions;
        public readonly List<TeamMember> TeamMembers;
        public readonly List<Team> Teams;

        //public Dictionary<int, List<TeamMemberModel>> TeamMembers;


        public TeamManager(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;

            Teams = new List<Team>();
            TeamDepartments = new List<DepartmentModel>();
            TeamMembers = new List<TeamMember>();
            TeamMemberPermissions = new List<TeamMemberPermissionModel>();
            Teams.Add(new Team
            {
                Id = 0,
                Name = " - keine -"
            });

            if (!_databaseContext.TeamModel.Local.Any()) return;
            foreach (var team in _databaseContext.TeamModel.Local.ToList())
            {
                //Logger.Debug($"Loaded Team \"{team.Name}\"");
                switch (team.Id)
                {
                    case 1:
                        AddTeam<Lspd>(team);
                        break;
                    default:
                        AddTeam<Team>(team);
                        break;
                }
            }

            if (_databaseContext.TeamMemberPermissionModel.Local.Any())
                TeamMemberPermissions.AddRange(_databaseContext.TeamMemberPermissionModel.Local);
        }

        private void AddTeam<T>(global::server.Teams.Team team) 
            where T: global::server.Teams.Team
        {
            Teams.Add(_mapper.Map<T>(team));
        }

        public T GetTeam<T>(int teamId) 
            where T: global::server.Teams.Team
        {
            return Teams.Find(x => x.Id == teamId) as T;
        }

        public DepartmentModel GetTeamDepartment(int departmentId)
        {
            return TeamDepartments.Find(x => x.Id == departmentId);
        }

        public TeamMember GetTeamMember(int memberId)
        {
            return TeamMembers.Find(x => x.Id == memberId);
        }

        public List<global::server.Teams.Team> GetTeamsForPlayer(Character player)
        {
            var teams = new List<global::server.Teams.Team>();

            foreach (var teamMember in GetTeamMembersForPlayer(player)) teams.Add(GetTeam<global::server.Teams.Team>(teamMember.Id));

            return teams;
        }

        public List<DepartmentModel> GetDepartmentsForPlayer(Character player)
        {
            var departments = new List<DepartmentModel>();

            foreach (var teamMember in GetTeamMembersForPlayer(player))
                departments.Add(GetTeamDepartment(teamMember.DepartmentId));

            return departments;
        }

        public IEnumerable<TeamMember> GetTeamMembersForPlayer(Character player)
        {
            return TeamMembers.FindAll(x => x.CharacterId == player.Id);
        }
        public List<TeamDto> GetTeamsIPlayer()
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

        public void SendToIPlayer(IPlayer player)
        {
            player.Emit("Teams:Receive", JsonConvert.SerializeObject(GetTeamsIPlayer()));
        }
    }
}