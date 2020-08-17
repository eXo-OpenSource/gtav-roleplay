using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Teams
{
    [Table("Departments")]
    public class DepartmentModel
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}