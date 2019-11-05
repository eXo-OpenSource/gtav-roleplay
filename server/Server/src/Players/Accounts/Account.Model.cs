using System.ComponentModel.DataAnnotations.Schema;
using server.Enums;
using Character = server.Players.Characters.Character;

namespace server.Players.Accounts
{
    [Table("Accounts")]
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EMail { get; set; }
        public AdminLevel AdminLvl { get; set; }
        public string SocialClubName { get; set; }
        public int ForumId { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public string Serial { get; set; }

        [Column(TypeName = "tinyint(1)")]
        public bool Autologin { get; set; }
    }
}