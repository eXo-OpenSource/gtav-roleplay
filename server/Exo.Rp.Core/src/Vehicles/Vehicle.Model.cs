using System;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Data;
using AltV.Net.Enums;
using models.Enums;
using server.Inventory.Inventories;

namespace server.Vehicles
{
    [Table("Vehicles")]
    public partial class Vehicle
    {
        public int Id { get; set; }
        public Int64 Model { get; set; }
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

        [ForeignKey("InventoryModel")]
        public int InventoryId { get; set; }
        public VehicleInventory InventoryModel { get; set; }

        private VehicleInventory _vehicleInventory { get; set; }

        [NotMapped]
        public VehicleModel VehicleModel
        {
            get => (VehicleModel) Model;
            set => Model = (Int64) value;
        }

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

        [NotMapped]
        public VehicleInventory Inventory
        {
            get => _vehicleInventory;
            set
            {
                _vehicleInventory = value;
                InventoryModel = value;
            }
        }
    }
}
 