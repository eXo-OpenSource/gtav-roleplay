using System;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Data;
using AltV.Net.Enums;
using server.Shops;

namespace server.Vehicles
{
    [Table("ShopVehicles")]
    public class VehicleShopVehicle
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotX { get; set; }
        public float RotY { get; set; }
        public float RotZ { get; set; }
        public int Price { get; set; }

        public int ShopId { get; set; }

        [NotMapped]
        public VehicleModel Vehicle
        {
            get => (VehicleModel) Enum.Parse(typeof(VehicleModel), ModelName);
            set => ModelName = value.ToString();
        }

        [NotMapped]
        public Shop Shop
        {
            get => ShopId > 0 ? Core.GetService<ShopManager>().Get<Shop>(ShopId) : null;
            set => ShopId = value.Id;
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
        public Rotation Rot
        {
            get => new Rotation(RotX, RotY, RotZ);
            set
            {
                RotX = value.Yaw;
                RotY = value.Roll;
                RotZ = value.Pitch;
            }
        }
    }
}