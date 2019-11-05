using System;
using System.IO;
using models.Utils.Serialization;

namespace models.Teams
{
    public class DepartmentDto : Serializable<DepartmentDto>
    {
        public string Description;
        public int Id;
        public string Name;
        public int Rank;
        
        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Description);
                    writer.Write(Id);
                    writer.Write(Name);
                    writer.Write(Rank);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override DepartmentDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Description = reader.ReadString();
                    Id = reader.ReadInt32();
                    Name = reader.ReadString();
                    Rank = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}
