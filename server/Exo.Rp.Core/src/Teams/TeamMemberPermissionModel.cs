using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Teams
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