using System;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

using server.Players;
using server.Players.Characters;

namespace server.Jobs.Jobs
{
    internal class Tree
    {
        public Tree(Position treeCenter)
        {
            Center = treeCenter;
           /* Col = NAPI.ColShape.CreateSphereColShape(Center, 2f, 0);
            Col.OnEntityEnterColShape += OnEnterCol;
            Col.OnEntityExitColShape += OnExitCol;*/
            LastUsed = DateTime.Now.AddSeconds(-Farmer.TreeCooldown);
        }

        private Position Center { get; }
        private ColShape Col { get; }
        private DateTime LastUsed { get; set; }
        private int InteractionId { get; set; }

        private void OnEnterCol(ColShape shape, IPlayer player)
        {
            if (player.GetCharacter().IsJobCurrentAndActive(default(Farmer))) return;

            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            InteractionId = player.GetCharacter().ShowInteraction("Apfelbaum", "JobFarmer:onTreeInteract",
                interactionData: interactionData, text: "Drücke E um einen Apfel zu pflücken!");
        }

        private void OnExitCol(ColShape shape, IPlayer player)
        {
            if (player.GetCharacter() == null || !(player.GetCharacter().GetJob() is Farmer) ||
                !player.GetCharacter().IsJobActive()) return;

            if (InteractionId == -1) return;

            player.GetCharacter().HideInteraction(InteractionId);
            InteractionId = -1;
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
