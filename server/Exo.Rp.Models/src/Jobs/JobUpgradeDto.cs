using System;
using System.IO;
using serialization;

namespace models.Jobs
{
	public class JobUpgradeDto : Serializable<JobUpgradeDto>
	{
		public int Id { get; set; }
		public int Points { get; set; }
		public int Value { get; set; }
		public string Text { get; set; }

		public override string SerializeObject()
		{
			using (var memoryStream = new MemoryStream())
			{
				using (var writer = new BinaryWriter(memoryStream))
				{
					writer.Write(typeof(JobUpgradeDto).Name);

					writer.Write(Id);
					writer.Write(Points);
					writer.Write(Value);
					writer.Write(Text);

					return Convert.ToBase64String(memoryStream.ToArray());
				}
			}
		}

		public override JobUpgradeDto DeserializeObject(string value)
		{
			using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
			{
				using (var reader = new BinaryReader(memoryStream))
				{
					var type = reader.ReadString();

					if (type != typeof(JobUpgradeDto).Name)
						throw new ArgumentException($"Expected data from type {typeof(JobUpgradeDto).Name} got {type}.");

					Id = reader.ReadInt32();
					Points = reader.ReadInt32();
					Value = reader.ReadInt32();
					Text = reader.ReadString();

					return this;
				}
			}
		}
	}
}
