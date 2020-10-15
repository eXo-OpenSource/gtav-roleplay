using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Characters;
using Exo.Rp.Models.Enums;
using Exo.Rp.Core.Streamer.Entities;
using Exo.Rp.Core.Streamer.Private;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Jobs.Jobs
{
    internal class Tree
    {
        public Tree(Position treeCenter)
        {
            Center = treeCenter;
            Col = (Colshape.Colshape) Alt.CreateColShapeSphere(Center, 2);
            Col.OnColShapeEnter += OnEnterCol;
            Col.OnColShapeExit += OnExitCol;
            LastUsed = DateTime.Now.AddSeconds(-Farmer.TreeCooldown);
            Blip = new PrivateBlip(Center, 0, 300) { Sprite = 364, Name = "Apfelbaum" };
            Core.GetService<PrivateStreamer>().AddEntity(Blip);
        }

        private Position Center { get; }
        private Colshape.Colshape Col { get; }
        private DateTime LastUsed { get; set; }
        private string InteractionId { get; set; }
        public PrivateEntity Blip { get; set; }

        private void OnEnterCol(Colshape.Colshape colshape, IEntity entity)
        {
            if (!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || player.GetCharacter().GetJob().JobId != (int)JobId.Farmer ||
                !player.GetCharacter().IsJobActive() || player.IsInVehicle) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };

            InteractionId = player.GetCharacter().ShowInteraction("Apfelbaum", "JobFarmer:onTreeInteract",
                interactionData: interactionData, text: "Drücke E um einen Apfel zu pflücken!");
        }

        private void OnExitCol(Colshape.Colshape colshape, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null || !(player.GetCharacter().GetJob() is Farmer) ||
                !player.GetCharacter().IsJobActive()) return;

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
            return (int)(Farmer.TreeCooldown - (DateTime.Now - LastUsed).TotalSeconds);
        }

        public void Destroy(IPlayer player)
        {
            Col.Remove();
            Core.GetService<PrivateStreamer>().RemoveEntity(Blip);
            Blip.RemoveVisibleEntity(player.Id);
        }
    }
}