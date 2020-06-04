using System.Numerics;
using AltV.Net.EntitySync;

namespace server.Streamer.Entities
{
	public class StreamObject : Entity, IEntity
	{

		public uint? Model
		{
			get => !TryGetData("model", out uint model) ? (uint?) null : model;
			set => SetData("model", value);
		}

		public StreamObject(Vector3 position, int dimension, uint range) : base(1, position, dimension, range)
		{
		}
	}
}
