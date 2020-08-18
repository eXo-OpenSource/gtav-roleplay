using System;
using System.ComponentModel.DataAnnotations.Schema;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.World
{
    [Table("WorldObjects")]
    public class WorldObject
    {
        public int Id { get; set; }
        public WorldObjects Type { get; set; }
        public string Position { get; set; }
        public string Rotation { get; set; }
        public int PlacedBy { get; set; }
        public DateTime Date { get; set; }
    }
}