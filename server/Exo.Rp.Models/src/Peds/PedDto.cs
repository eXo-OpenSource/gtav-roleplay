using System;
using System.IO;
using Exo.Rp.Serialization;

namespace Exo.Rp.Models.Peds
{
    [Serializable]
    public class PedDto : Serializable<PedDto>
    {
        public uint Skin { get; set; }
        public string Name { get; set; }
        public string Pos { get; set; }
        public float Rot { get; set; }
        public int Type { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Skin);
                    writer.Write(Name);
                    writer.Write(Pos);
                    writer.Write(Rot);
                    writer.Write(Type);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override PedDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Skin = reader.ReadUInt32();
                    Name = reader.ReadString();
                    Pos = reader.ReadString();
                    Rot = reader.ReadSingle();
                    Type = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}