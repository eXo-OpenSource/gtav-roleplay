using System.Collections.Generic;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using models.Enums;
using models.Peds;
using Newtonsoft.Json;
using server.Database;
using server.Extensions;

namespace server.Peds
{
    internal class PedManager
    {
        public static readonly Dictionary<int, Ped> Peds;

        static PedManager()
        {
            Peds = new Dictionary<int, Ped>();

            if (!ContextFactory.Instance.PedModel.Local.Any()) return;
            foreach (var pedM in ContextFactory.Instance.PedModel.Local) Peds.Add(pedM.Id, pedM);
        }

        public static List<PedDto> GetPedsForIPlayer()
        {
            var pedList = new List<PedDto>();
            foreach (var pedObject in Peds.Values)
            {
                var nPed = new PedDto()
                {
                    //Id = pedObject.Id,
                    Skin = (uint) pedObject.Skin,
                    Name = pedObject.Name,
                    Pos = new Position(pedObject.PosX, pedObject.PosY, pedObject.PosZ).Serialize(),
                    Rot = pedObject.Rot,
                    Type = (int) pedObject.Type
                };
                pedList.Add(nPed);
            }
            return pedList;
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
            ContextFactory.Instance.PedModel.Local.Add(ped);
            return ped;
        }

        public static void SendToIPlayer(IPlayer player)
        {
            player.Emit("Peds:Init", JsonConvert.SerializeObject(GetPedsForIPlayer()));
        }
    }
}