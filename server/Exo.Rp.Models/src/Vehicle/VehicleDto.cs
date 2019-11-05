using System;
using System.IO;
using models.Enums;
using serialization;

namespace models.Vehicle
{
    [Serializable]
    public class VehicleDto : Serializable<VehicleDto>
    {
        public int Id { get; set; }
        public int Vehicle { get; set; }
        public OwnerType OwnerType { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string ModelName { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(Vehicle);
                    writer.Write((int)OwnerType);
                    writer.Write(OwnerId);
                    writer.Write(OwnerName);
                    writer.Write(ModelName);
 
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override VehicleDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Id = reader.ReadInt32();
                    Vehicle = reader.ReadInt32();
                    OwnerType = (OwnerType)reader.ReadInt32();
                    OwnerId = reader.ReadInt32();
                    OwnerName = reader.ReadString();
                    ModelName = reader.ReadString();

                    return this;
                }
            }
        }
    }
}
