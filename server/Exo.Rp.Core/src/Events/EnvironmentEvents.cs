using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using server.Environment;
using server.Players;
using server.Vehicles;
using server.Vehicles.Types;
using System;
using System.Runtime.Versioning;

namespace server.Events

{
    internal class EnvironmentEvents : IScript
    {
        [Event("onTownHallInteraction")]
        public void OnVehicleShopInteraction(IPlayer player)
        {
            var townHall = (TownHall) player.GetCharacter().GetInteractionData().SourceObject;
            TownHall.OnInteract(player);
        }

        [Event("setCharacterName")]
        public void OnVehicleShopInteraction(IPlayer player, string type, string name)
        {
            switch (type)
            {
                case "first":
                    player.GetCharacter().FirstName = name;
                    player.SendInformation($"Du hast deinen Vornamen in {name} geändert!");
                    break;
                case "last":
                    player.GetCharacter().LastName = name;
                    player.SendInformation($"Du hast deinen Nachnamen in {name} geändert!");
                    break;
            }

            player.GetCharacter().Save();
        }

		[Event("CarRent:OnPedInteract")]
		public void OnColshapeHit(IPlayer player)
		{
			player.Emit("CarRent:OpenUI");
		}

		[Event("CarRent:SpawnVehicle")]
		public Vehicle RentVehicle(IPlayer player, string _vehicle)
		{
			//VehicleModel vehicle = (VehicleModel)Enum.Parse(typeof(VehicleModel), _vehicle); 
			//var veh = Core.GetService<VehicleManager>().CreateRentedVehicle(VehicleModel.Adder, new Position(-986.8756713867188f, -2690.510986328125f, 14.04065227508545f), 0, 5000, Rgba.Zero, Rgba.Zero);

			var veh = Core.GetService<VehicleManager>().CreateRentedVehicle(VehicleModel.Adder, new Position(-986.8756713867188f, -2690.510986328125f, 14.04065227508545f), 0, 50000, Rgba.Zero, Rgba.Zero);
			return veh;
		}

	}
}
