using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Teams
{
    [Table("TeamMemberPermissions")]
    public class TeamMemberPermissionModel
    {
        [Key] public int Id { get; set; }

        [ForeignKey("TeamMember")]
        public int TeamMemberId { get; set; }
        public TeamMember TeamMember { get; set; }

        public int PermissionId { get; set; }

        /*
        [NotMapped]
        public TeamMemberModel TeamMember
        {
            get => TeamManager.Instance.TeamMembers.Find(x => x.Data.id == TeamMemberId).Data;
            set => TeamMemberId = value.id;
        }
        */
    }
}