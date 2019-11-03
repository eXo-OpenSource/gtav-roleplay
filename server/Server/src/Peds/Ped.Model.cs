using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Data;
using AltV.Net.Enums;
using server.Enums;

namespace server.Peds
{
    [Table("Peds")]
    public partial class Ped
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }
        public int SkinId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Rot { get; set; }
        public PedType Type { get; set; }
        public int ObjectId { get; set; }

        [NotMapped]
        public PedModel Skin
        {
            get => (PedModel) SkinId;
            set => SkinId = (int) value;
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
    }
}