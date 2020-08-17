using System;
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Players.Characters;
using IPlayer = server.Players.IPlayer;

namespace server.Jobs.Jobs
{
	public class Stone
	{
		public Stone(Position stoneCenter)
		{
			Center = stoneCenter;
			Col = (Colshape.Colshape) Alt.CreateColShapeSphere(stoneCenter, 1);
			Col.OnColShapeEnter += OnEnterCol;
			Col.OnColShapeExit += OnExitCol;
			LastUsed = DateTime.Now.AddSeconds(-Miner.StoneCooldown);
		}

		public Position Center { get; set; }
		public Colshape.Colshape Col { get; set; }
		public DateTime LastUsed { get; set; }
		public string InteractionId { get; set; }

		private void OnEnterCol(Colshape.Colshape colshape, IEntity entity)
		{
			if(!(entity is IPlayer player)) return;
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

		private void OnExitCol(Colshape.Colshape colshape, IEntity entity)
		{
			if(!(entity is IPlayer player)) return;
			if (player.GetCharacter() != null && player.GetCharacter().GetJob() is Miner &&
				player.GetCharacter().IsJobActive())
				if (InteractionId != null)
				{
					player.GetCharacter().HideInteraction(InteractionId);
					InteractionId = null;
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
