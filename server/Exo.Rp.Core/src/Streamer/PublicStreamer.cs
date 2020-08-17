using System.Collections.Generic;
using AltV.Net;
using AltV.Net.EntitySync;
using server.Streamer.Entities;

namespace server.Streamer
{
	public class PublicStreamer : IManager
	{

		private List<StaticBlip> _globalBlips = new List<StaticBlip>();

		public PublicStreamer()
		{
			Alt.OnClient("ready", (player, args) =>
			{
				player.Emit("globalBlips:init", _globalBlips);
			});
		}
		public void AddObject(StreamObject entity)
		{
			AltEntitySync.AddEntity(entity);
		}

		public void RemoveObject(StreamObject streamObject)
		{
			AltEntitySync.RemoveEntity(streamObject);
		}

		public void AddBlip(StreamBlip blip)
		{
			AltEntitySync.AddEntity(blip);
		}

		public void RemoveBlip(StreamBlip blip)
		{
			AltEntitySync.RemoveEntity(blip);
		}

		public void AddGlobalBlip(StaticBlip blip)
		{
			_globalBlips.Add(blip);
		}
	}
}
