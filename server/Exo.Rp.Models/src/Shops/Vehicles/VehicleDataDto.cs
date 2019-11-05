using System;
using System.IO;
using AltV.Net.Elements.Entities;
using serialization;

namespace models.Shops.Vehicles
{
    [Serializable]
    public class VehicleDataDto : Serializable<VehicleDataDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public IVehicle Handle { get; set; }
        
        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(Name);
                    writer.Write(Price);
                    //writer.Write(Handle);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override VehicleDataDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Id = reader.ReadInt32();
                    Name = reader.ReadString();
                    Price = reader.ReadInt32();
                    //Handle = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}
