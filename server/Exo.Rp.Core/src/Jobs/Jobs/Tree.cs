using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Players.Characters;
using IPlayer = server.Players.IPlayer;

namespace server.Jobs.Jobs
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
        }

        private Position Center { get; }
        private Colshape.Colshape Col { get; }
        private DateTime LastUsed { get; set; }
        private string InteractionId { get; set; }

        private void OnEnterCol(Colshape.Colshape colshape, IEntity entity)
        {
	        if(!(entity is IPlayer player)) return;
            if (player.GetCharacter().IsJobCurrentAndActive<Farmer>()) return;

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
    }
}
