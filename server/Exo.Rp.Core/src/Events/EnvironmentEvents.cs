using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Enums;
using server.Environment;
using server.Players;

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
        public void RentVehicle(IPlayer player, string vehicle, int price)
        {
            CarRent.SpawnVehicle(player, vehicle, price);
        }

    }
}