using System;
using System.IO;
using Exo.Rp.Models.Enums;
using Exo.Rp.Serialization;
using Gender = Exo.Rp.Models.Enums.Gender;

namespace Exo.Rp.Models.Players
{
    [Serializable]
    public class PlayerDto : Serializable<PlayerDto>
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public AdminLevel AdminLevel { get; set; }
        public int InventoryId { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(PlayerId);
                    writer.Write(UserName);
                    writer.Write(Firstname);
                    writer.Write(Lastname);
                    writer.Write((int)Gender);
                    writer.Write((int)AdminLevel);
                    writer.Write(InventoryId);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override PlayerDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Id = reader.ReadInt32();
                    PlayerId = reader.ReadInt32();
                    UserName = reader.ReadString();
                    Firstname = reader.ReadString();
                    Lastname = reader.ReadString();
                    Gender = (Gender) reader.ReadInt32();
                    AdminLevel = (AdminLevel) reader.ReadInt32();
                    InventoryId = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}