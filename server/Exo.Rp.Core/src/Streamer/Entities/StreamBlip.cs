using System.Numerics;
using AltV.Net.EntitySync;

namespace server.Streamer.Entities
{
	public class StreamBlip : Entity, IEntity
	{
		public int? Sprite
		{
			get
			{
				if (!TryGetData("sprite", out int sprite))
					return null;

				return sprite;
			}
			set => SetData("sprite", value);
		}

		public string Name
		{
			get
			{
				if (!TryGetData("name", out string name))
					return null;

				return name;
			}
			set => SetData("name", value);
		}

		public StreamBlip(Vector3 position, int dimension, uint range) : base(0, position, dimension, range)
		{
		}
	}
}
