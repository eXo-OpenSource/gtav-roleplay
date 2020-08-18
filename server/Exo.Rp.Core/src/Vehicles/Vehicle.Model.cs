using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Data;
using AltV.Net.Enums;
using Exo.Rp.Core.Inventory.Inventories;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Vehicles
{
    [Table("Vehicles")]
    public partial class Vehicle
    {
        public int Id { get; set; }
        public VehicleModel Model { get; set; }
        public OwnerType OwnerType { get; set; }
        public int OwnerId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotZ { get; set; }
        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public string Plate { get; set; }

        [Column(TypeName = "tinyint(1)")]
        public bool Locked { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }
        public VehicleInventory Inventory { get; set; }

        [NotMapped]
        public Position Pos
        {
            get => new Position(PosX, PosY, PosZ);
            set
            {
                PosX = value.X;
                PosY = value.Y;
                PosZ = value.Z;
            }
        }
    }
}