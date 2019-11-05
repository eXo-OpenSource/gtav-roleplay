using System;
using System.Collections.Generic;
using System.IO;
using models.Utils.Extensions;
using models.Utils.Serialization;

namespace models.Inventory
{
    [Serializable]
    public class InventoryDto : Serializable<InventoryDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Size { get; set; }
        public List<BagDto> Bags { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(Title);
                    writer.Write(Size);
                    writer.Write(Bags);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override InventoryDto DeserializeObject(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            {
                using (var reader = new BinaryReader(memoryStream))
                {
                    var type = reader.ReadString();

                    if (type != GetClassName())
                        throw new ArgumentException($"Expected data from type {GetClassName()} got {type}.");

                    Id = reader.ReadInt32();
                    Title = reader.ReadString();
                    Size = reader.ReadInt32();
                    Bags = reader.ReadListSerializable<BagDto>();

                    return this;
                }
            }
        }
    }

    [Serializable]
    public class BagDto : Serializable<BagDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool Selected { get; set; }
        public Dictionary<int, ItemDto> Items { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(Name);
                    writer.Write(Icon);
                    writer.Write(Selected);
                    writer.Write(Items);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override BagDto DeserializeObject(string value)
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
                    Icon = reader.ReadString();
                    Selected = reader.ReadBoolean();
                    Items = reader.ReadDictionarySerializable<int, ItemDto>();

                    return this;
                }
            }
        }
    }

    [Serializable]
    public class ItemDto : Serializable<ItemDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Amount { get; set; }
        public string Subtext { get; set; }

        public override string SerializeObject()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(memoryStream))
                {
                    writer.Write(GetClassName());

                    writer.Write(Id);
                    writer.Write(Name);
                    writer.Write(Icon);
                    writer.Write(Amount);
                    writer.Write(Subtext);

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public override ItemDto DeserializeObject(string value)
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
                    Icon = reader.ReadString();
                    Amount = reader.ReadInt32();
                    Subtext = reader.ReadString();

                    return this;
                }
            }
        }
    }
}