using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.World
{
    [Table("WorldObjects")]
    public class WorldObject
    {
        public int Id { get; set; }
        public models.Enums.WorldObjects Type { get; set; }
        public string Position { get; set; }
        public string Rotation { get; set; }
        public int PlacedBy { get; set; }
        public DateTime Date { get; set; }
    }
}
