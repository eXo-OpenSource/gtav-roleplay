using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exo.Rp.Core.Players.Characters
{
    [Table("Licenses")]
    public class Licenses
    {
        [Key]
        public int Id { get; set; }

        public int Greencard { get; set; }
        public int Citizenship { get; set; }
        public int Car { get; set; }
        public int Truck { get; set; }
        public int Motorcycle { get; set; }

    }
}