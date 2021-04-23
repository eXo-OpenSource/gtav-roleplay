using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Peds;
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
        public Position PedPosition;
        public PedModel PedModel;
        public string InteractionId;

        public Cityhall(int townHallId)
        {
            Id = townHallId;
        }

        public void Init()
        {
            PedManager.CreateRuntimePed(PedModel, PedPosition, 180, 0);
            var col = (Colshape.Colshape)Alt.CreateColShapeSphere(PedPosition, 3);
            col.OnColShapeEnter += OnColEnter;
            col.OnColShapeExit += OnColExit;
            Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
            {
                Name = Name,
                Color = 4,
                SpriteId = SpriteId,
                X = PedPosition.X,
                Y = PedPosition.Y,
                Z = PedPosition.Z
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
                .ShowInteraction(Name, "Cityhall:OnEntranceInteract", interactionData: interactionData);
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