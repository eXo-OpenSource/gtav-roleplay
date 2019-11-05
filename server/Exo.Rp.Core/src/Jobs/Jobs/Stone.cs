using System;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Players;
using server.Players.Characters;

namespace server.Jobs.Jobs
{
    public class Stone
    {
        public Stone(Position stoneCenter)
        {
            Center = stoneCenter;
            //Col = AltV.Net.Alt.Exo.RP.Server.CreateColShapeSphere(stoneCenter, 1);
            //Col.OnEntityEnterColShape += OnEnterCol;
            //Col.OnEntityExitColShape += OnExitCol;
            LastUsed = DateTime.Now.AddSeconds(-Miner.StoneCooldown);
        }

        public Position Center { get; set; }
        public ColShape Col { get; set; }
        public DateTime LastUsed { get; set; }
        public int InteractionId { get; set; }

        private void OnEnterCol(ColShape shape, IPlayer player)
        {
            if (player.GetCharacter() != null && player.GetCharacter().GetJob() is Miner &&
                player.GetCharacter().IsJobActive())
            {
                var interactionData = new InteractionData
                {
                    SourceObject = this,
                    CallBack = null
                };
                InteractionId = player.GetCharacter().ShowInteraction("Stein", "JobMiner:onStoneInteract",
                    interactionData: interactionData, text: "Drücke E um den Stein zu zerschlagen!");
            }
        }

        private void OnExitCol(ColShape shape, IPlayer player)
        {
            if (player.GetCharacter() != null && player.GetCharacter().GetJob() is Miner &&
                player.GetCharacter().IsJobActive())
                if (InteractionId != -1)
                {
                    player.GetCharacter().HideInteraction(InteractionId);
                    InteractionId = -1;
                }
        }

        public bool IsUsable()
        {
            return (DateTime.Now - LastUsed).TotalSeconds > Miner.StoneCooldown;
        }

        public void Use()
        {
            LastUsed = DateTime.Now;
        }

        public int Cooldown()
        {
            return (int)(Miner.StoneCooldown - (DateTime.Now - LastUsed).TotalSeconds);
        }
    }
}
