using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System.Collections.Generic;
using IPlayer = server.Players.Player;
using server.Players.Characters;

namespace server.Environment
{
	internal class CarRent : IScript
	{
		int loadedCarRents = 0;

		private Dictionary<int, Colshape.Colshape> colshapes;

		private readonly Position[] rentSpots =
		{
			new Position(-986.8756713867188f, -2690.510986328125f, 14.04065227508545f)
		};

		public CarRent()
		{
			colshapes = new Dictionary<int, Colshape.Colshape>();
			LoadCarRents();
		}

		public void LoadCarRents()
		{
			foreach (Position _carRent in rentSpots)
			{
				Alt.Log(_carRent.ToString());
				Alt.CreateBlip(BlipType.Cont, _carRent);
				colshapes.Add(loadedCarRents, (Colshape.Colshape)Alt.CreateColShapeSphere(_carRent, 1.9f));
				colshapes[loadedCarRents].OnColShapeEnter += OnColshapeEnter;
				loadedCarRents++;
			}
			Alt.Log($"Loaded {loadedCarRents} carRents");
		}

		public void OnColshapeEnter(Colshape.Colshape col, IEntity entity)
		{
			if (!(entity is IPlayer player)) return;

			var interactionData = new InteractionData {
				SourceObject = this,
				CallBack = null
			};

			player.GetCharacter().ShowInteraction("Rent A Car", "CarRent:OnPedInteract", "Drücke E um zu interagieren", interactionData: interactionData);
		}
	}
}
