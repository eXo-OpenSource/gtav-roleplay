using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using System.Collections.Generic;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Environment
{
    public class Cityhall
    {
        public string Name;
        public int Id;
        public int SpriteId;
        public Position EntranceMarkerPos;
        public Position EntranceSpawnPos;
        public Position ExitMarkerPos;
        public Position ExitSpawnPos;
        public string InteractionId;
        public Cityhall(int townHallId)
        {
            Id = townHallId;
        }

        public void Init()
        {
            var col = (Colshape.Colshape)Alt.CreateColShapeSphere(EntranceMarkerPos, 3);
            col.OnColShapeEnter += OnColEnter;
            col.OnColShapeExit += OnColExit;

            Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
            {
                Name = Name,
                Color = 4,
                SpriteId = SpriteId,
                X = EntranceMarkerPos.X,
                Y = EntranceMarkerPos.Y,
                Z = EntranceMarkerPos.Z
            });
        }

        public void OnColEnter(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            InteractionId = player.GetCharacter()
                .ShowInteraction(Name, "TownHall:OnEntranceInteract", interactionData: interactionData);
        }
        public void OnColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(InteractionId);
        }

        public void OnInteract(IPlayer player)
        {
            player.SendInformation("hi");
        }
    }
}