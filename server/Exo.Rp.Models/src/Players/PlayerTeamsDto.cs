using System;
using System.IO;
using serialization;

namespace models.Players
{
    [Serializable]
    public class PlayerTeamsDto : Serializable<PlayerTeamsDto>
    {
        public int TeamId { get; set; }
        public int Rank { get; set; }
        public int DepartmentId { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(TeamId);
                    writer.Write(Rank);
                    writer.Write(DepartmentId);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override PlayerTeamsDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    TeamId = reader.ReadInt32();
                    Rank = reader.ReadInt32();
                    DepartmentId = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}
