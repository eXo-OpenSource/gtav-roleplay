using System;
using System.Collections.Generic;
using System.IO;
using models.Utils.Extensions;
using models.Utils.Serialization;

namespace models.Teams
{
    public class TeamDto : Serializable<TeamDto>
    {
        public List<DepartmentDto> Departments;
        public string Description;
        public int Id;
        public string Name;
        
        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Departments);
                    writer.Write(Description);
                    writer.Write(Id);
                    writer.Write(Name);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override TeamDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Departments = reader.ReadListSerializable<DepartmentDto>();
                    Description = reader.ReadString();
                    Id = reader.ReadInt32();
                    Name = reader.ReadString();

                    return this;
                }
            }
        }
    }
}
