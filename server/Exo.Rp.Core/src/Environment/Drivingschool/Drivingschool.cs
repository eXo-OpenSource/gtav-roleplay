﻿using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Peds;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Environment
{
    public class Drivingschool
    {
        public string Name;
        public int Id;
        public int SpriteId;
        public Position LaptopPosition;
        public Position PedPosition;
        public string InteractionId;
        public PedModel PedModel;

        public Drivingschool(int drivingschoolId)
        {
            Id = drivingschoolId;
        }

        public void Init()
        {
            PedManager.CreateRuntimePed(PedModel.Cop01SFY, PedPosition, 0, 0);
            var col = (Colshape.Colshape)Alt.CreateColShapeSphere(PedPosition, 3);
            var examCol = (Colshape.Colshape)Alt.CreateColShapeSphere(LaptopPosition, 3);
            col.OnColShapeEnter += OnColEnter;
            col.OnColShapeExit += OnColExit;
            examCol.OnColShapeEnter += OnLaptopColEnter;
            examCol.OnColShapeExit += OnLaptopColExit;
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
                .ShowInteraction(Name, "Drivingschool:OnPedInteract", interactionData: interactionData);
        }
        public void OnColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(InteractionId);
        }
        public void OnLaptopColEnter(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            InteractionId = player.GetCharacter()
                .ShowInteraction(Name, "Drivingschool:OnLaptopInteract", $"Drücke E um die Fahrprüfung für ${(int)LicensePrice.Car} zu starten", interactionData: interactionData);
        }
        public void OnLaptopColExit(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            player.GetCharacter().HideInteraction(InteractionId);
        }

        public void OnExamFinished(IPlayer player, int score, License license)
        {
            if (score >= 80)
            {
                player.GetCharacter().SetPlayerLicense(license, License.Add);
                player.SendSuccess($"Glückwunsch zur Fahrerlaubnis! Du hast mit {score}% bestanden!");
            }
            else
                player.SendError($"Durchgefallen! Du bist mit {score}% durchgefallen!");
        }

        public void StartDrivingExam(IPlayer player, License license)
        {
            if (player.GetCharacter().GetMoney(true) >= (int)license)
            {
                player.GetCharacter().TakeMoney((int)license, "Drivingschool Exam", true);
                player.SendNotification("Das Geld wurde Dir vom Konto abgebucht.");
                player.Emit("Drivingschool:OpenUi", (int)license);
            }
            else
            {
                player.SendError($"Die Fahrprüfung kostet ${LicensePrice.Car}!");
            }
        }
    }
}