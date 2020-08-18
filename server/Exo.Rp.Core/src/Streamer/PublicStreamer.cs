using System;
using System.Collections.Generic;
using AltV.Net;
using AltV.Net.EntitySync;
using Exo.Rp.Core.Streamer.Entities;

namespace Exo.Rp.Core.Streamer
{
    public class PublicStreamer : IManager
    {

        private List<StaticBlip> _globalBlips = new List<StaticBlip>();

        public PublicStreamer()
        {
            Alt.OnClient<int>("ready", (player, _) =>
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

        public void AddPed(StreamPed ped)
        {
            AltEntitySync.AddEntity(ped);
        }
    }
}