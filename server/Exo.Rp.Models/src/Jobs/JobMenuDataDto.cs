using System;
using System.Collections.Generic;
using System.IO;
using serialization;
using serialization.Extensions;

namespace models.Jobs
{
    public class JobMenuDataDto : Serializable<JobMenuDataDto>
    {
        public int Id { get; set; }
        public int CurrentJobId { get; set; }
        public string CurrentJobName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxPlayers { get; set; }
        public Dictionary<int, string> Players { get; set; }
        public int LeaderId { get; set; }
        public List<JobUpgradeCategoryDto> Upgrades { get; set; }
        public int JobPoints { get; set; }
        public Dictionary<int, int> PlayerUpgrades { get; set; }
        
        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(CurrentJobId);
                    writer.Write(CurrentJobName);
                    writer.Write(Name);
                    writer.Write(Description);
                    writer.Write(MaxPlayers);
                    writer.Write(Players);
                    writer.Write(LeaderId);
                    writer.Write(Upgrades);
                    writer.Write(JobPoints);
                    writer.Write(PlayerUpgrades);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override JobMenuDataDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Id = reader.ReadInt32();
                    CurrentJobId = reader.ReadInt32();
                    CurrentJobName = reader.ReadString();
                    Name = reader.ReadString();
                    Description = reader.ReadString();
                    MaxPlayers = reader.ReadInt32();
                    Players = reader.ReadDictionaryIConvertible<int, string>();
                    LeaderId = reader.ReadInt32();
                    Upgrades = reader.ReadListSerializable<JobUpgradeCategoryDto>();
                    JobPoints = reader.ReadInt32();
                    PlayerUpgrades = reader.ReadDictionaryIConvertible<int, int>();

                    return this;
                }
            }
        }
    }
}
