using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Streamer.Private;
using Exo.Rp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Jobs.Jobs
{
    public class Wood
    {
        public Wood(Position positon)
        {
            Position = positon;
            Col = (Colshape.Colshape)Alt.CreateColShapeSphere(positon, 2);
            Col.OnColShapeEnter += OnEnterCol;
            Col.OnColShapeExit += OnExitCol;
            LastUsed = DateTime.Now.AddSeconds(-Farmer.TreeCooldown);
            Blip = new PrivateBlip(positon, 0, 300) { Sprite = 364, Name = "Baum" };
            Core.GetService<PrivateStreamer>().AddEntity(Blip);
        }

        private Position Position { get; }
        public PrivateEntity Blip { get; set; }
        private Colshape.Colshape Col { get; }
        private DateTime LastUsed { get; set; }
        private string InteractionId { get; set; }

        private void OnEnterCol(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob().JobId != (int)JobId.WoodCutter ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };

            InteractionId = player.GetCharacter().ShowInteraction("Holz hacken", "JobWoodCutter:onTreeInteract",
                interactionData: interactionData, text: "Drücke E um das Holz zu Hacken!");
        }

        private void OnExitCol(Colshape.Colshape col, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob().JobId != (int)JobId.WoodCutter ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

            if (InteractionId == null) return;

            player.GetCharacter().HideInteraction(InteractionId);
            InteractionId = null;
        }

        public bool IsUsable()
        {
            return Cooldown() < 0;
        }

        public void Use()
        {
            LastUsed = DateTime.Now;
        }

        public int Cooldown()
        {
            return (int)(WoodCutter.Cooldown - (DateTime.Now - LastUsed).TotalSeconds);
        }

        public void Destroy(IPlayer player)
        {
            Col.Remove();
            Core.GetService<PrivateStreamer>().RemoveEntity(Blip);
            Blip.RemoveVisibleEntity(player.Id);
        }

        public static readonly Position[] treePositions =
        {
            new Position(-577.217041015625f, 5469.3828125f, 60.45405197143555f),
            new Position(-579.6707153320312f, 5471.4169921875f, 59.69396209716797f),
            new Position(-572.5552978515625f, 5468.26171875f, 61.454185485839844f)
        };
    }
}
