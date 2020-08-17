using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Enums;
using models.Peds;
using Newtonsoft.Json;
using server.Database;
using server.Extensions;
using server.Streamer;
using server.Streamer.Entities;

namespace server.Peds
{
    internal class PedManager : IManager
    {
        public static readonly Dictionary<int, Ped> Peds;

        static PedManager()
        {
            Peds = new Dictionary<int, Ped>();

            if (!Core.GetService<DatabaseContext>().PedModel.Local.Any()) return;
            foreach (var pedM in Core.GetService<DatabaseContext>().PedModel.Local) Core.GetService<PublicStreamer>()
                .AddPed(new StreamPed(pedM.Pos, 0)
                {
                    Heading = pedM.Rot,
                    Model = (uint)pedM.Skin,
                    Static = true
                });
        }

        public static Ped GetFromType(PedType type, int objectId)
        {
            foreach (var pedObject in Peds.Values)
                if (pedObject.Type == type && pedObject.ObjectId == objectId)
                    return pedObject;
            return null;
        }

        public static Ped CreatePed(PedModel skin, Position position, int rot, PedType type)
        {
            var ped = new Ped
            {
                Skin = skin,
                Pos = position,
                Rot = rot,
                Type = type
            };
            Core.GetService<DatabaseContext>().PedModel.Local.Add(ped);
            return ped;
        }
    }
}