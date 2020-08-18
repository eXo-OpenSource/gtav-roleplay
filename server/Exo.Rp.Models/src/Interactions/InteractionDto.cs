using System;
using System.IO;
using Exo.Rp.Serialization;

namespace Exo.Rp.Models.Interactions
{
    [Serializable]
    public class InteractionDto : Serializable<InteractionDto>
    {
        public int Type = 3;
        public string Title { get; set; }
        public string Id { get; set; }
        public int ServerId { get; set; }
        public string ServerEvent { get; set; }
        public string Text { get; set; }
        public string Key { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Type);
                    writer.Write(Title);
                    writer.Write(Id);
                    writer.Write(ServerId);
                    writer.Write(ServerEvent);
                    writer.Write(Text);
                    writer.Write(Key);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override InteractionDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Type = reader.ReadInt32();
                    Title = reader.ReadString();
                    Id = reader.ReadString();
                    ServerId = reader.ReadInt32();
                    Text = reader.ReadString();
                    Key = reader.ReadString();

                    return this;
                }
            }
        }
    }
}