using System;
using System.IO;
using shared.Serialization;

namespace shared.Teams
{
    public class TeamMemberDto : Serializable<TeamMemberDto>
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int AccountId { get; set; }
        public int Rank { get; set; }


        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(DepartmentId);
                    writer.Write(AccountId);
                    writer.Write(Rank);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override TeamMemberDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Id = reader.ReadInt32();
                    DepartmentId = reader.ReadInt32();
                    AccountId = reader.ReadInt32();
                    Rank = reader.ReadInt32();

                    return this;
                }
            }
        }
    }
}
