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
        public Position EntrancePosition;
        public Position ExitPosition;
        public Position ExitSpawn;
        public PedModel PedModel;
        public string EntranceInteractionId;
        public string ExitInteractionId;
        public string LicensesInteractionId;

        public Cityhall(int townHallId)
        {
            Id = townHallId;
        }

        public void Init()
        {
            PedManager.CreatePed(PedModel, PedPosition, 180, 0);
            var col = (Colshape.Colshape)Alt.CreateColShapeSphere(EntrancePosition, 3);
            var interiorCol = (Colshape.Colshape)Alt.CreateColShapeSphere(ExitPosition, 3);
            var licensesCol = (Colshape.Colshape)Alt.CreateColShapeSphere(PedPosition, 3);
            col.OnColShapeEnter += OnColEnter;
            col.OnColShapeExit += OnColExit;
            interiorCol.OnColShapeEnter += OnInteriorColEnter;
            interiorCol.OnColShapeExit += OnInteriorColExit;
            licensesCol.OnColShapeEnter += OnLicensesColEnter;
            licensesCol.OnColShapeExit += OnLicensesColExit;

            Core.GetService<PublicStreamer>().AddGlobalBlip(new StaticBlip
            {
                Name = Name,
                Color = 4,
                SpriteId = SpriteId,
                X = EntrancePosition.X,
                Y = EntrancePosition.Y,
                Z = EntrancePosition.Z
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
            EntranceInteractionId = player.GetCharacter()
                .ShowInteraction(Name, "Cityhall:OnEntranceInteract", interactionData: interactionData);
        }

        public void OnColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(EntranceInteractionId);
        }

        public void OnInteriorColEnter(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            ExitInteractionId = player.GetCharacter()
                .ShowInteraction(Name, "Cityhall:OnExitInteract", interactionData: interactionData);
        }

        public void OnInteriorColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(ExitInteractionId);
        }

        public void OnLicensesColEnter(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            LicensesInteractionId = player.GetCharacter()
                .ShowInteraction(Name, "Cityhall:OnLicensesPedInteract", interactionData: interactionData);
        }

        public void OnLicensesColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(LicensesInteractionId);
        }

        public void OnLicensesPedInteract(IPlayer player)
        {
            player.Emit("Cityhall:OpenUi");
        }

        public void Enter(IPlayer player)
        {
            player.SendInformation("Du hast die Stadthalle betreten.");
            player.SetPosition(-555.3887f, -204.8264f, 30.11124f);
        }

        public void Leave(IPlayer player)
        {
            player.SendInformation("Du hast die Stadthalle verlassen.");
            player.Position = ExitSpawn;
        }
    }
}