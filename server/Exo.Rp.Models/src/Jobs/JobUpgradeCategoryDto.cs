using System;
using System.Collections.Generic;
using System.IO;
using serialization;
using serialization.Extensions;

namespace models.Jobs
{
    public class JobUpgradeCategoryDto : Serializable<JobUpgradeCategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<JobUpgradeDto> Upgrades { get; set; }


        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(Name);
                    writer.Write(Description);
                    writer.Write(Upgrades);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override JobUpgradeCategoryDto DeserializeObject(string value)
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
                    Description = reader.ReadString();
                    Upgrades = reader.ReadListSerializable<JobUpgradeDto>();

                    return this;
                }
            }
        }
    }
}