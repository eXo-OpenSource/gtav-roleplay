using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Enums;

namespace server.Teams
{
    [Table("TeamMemberPermissions")]
    public class TeamMemberPermissionModel
    {
        [Key] public int Id { get; set; }

        [ForeignKey("TeamMember")]
        public int TeamMemberId { get; set; }
        public TeamMember TeamMember { get; set; }

        public TeamPermissions Permissions { get; set; }
    }
}