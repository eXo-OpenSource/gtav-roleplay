using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using server.Players.Characters;

namespace server.Teams
{
    [Table("TeamMembers")]
    public partial class TeamMember
    {
        [Key] public int Id { get; set; }

        public int Rank { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public DepartmentModel Department { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        /*
        [NotMapped]
        public TeamModel Team
        {
            get => TeamId > 0 ? TeamManager.Instance.GetTeam(TeamId).Data : null;
            set => TeamId = value.id;
        }

        [NotMapped]
        public TeamDepartmentModel Department
        {
            get => DepartmentId > 0 ? TeamManager.Instance.GetTeam(TeamId).Departments[DepartmentId].Data : null;
            set => DepartmentId = value.id;
        }

        [NotMapped] public IPlayer IPlayer => CharacterId > 0 ? PlayerManager.Instance.GetIPlayer(CharacterId) : null;
        */
    }
}