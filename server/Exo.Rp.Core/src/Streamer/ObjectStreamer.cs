using AltV.Net.EntitySync;
using server.Streamer.Entities;

namespace server.Streamer
{
	public class ObjectStreamer : IManager
	{
		public void Add(StreamObject entity)
		{
			AltEntitySync.AddEntity(entity);
		}

		public void Remove(StreamObject streamObject)
		{
			AltEntitySync.RemoveEntity(streamObject);
		}
	}
}
