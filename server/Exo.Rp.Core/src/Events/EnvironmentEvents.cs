using AltV.Net;
using Exo.Rp.Core.Environment;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Environment.CarRentTypes;

namespace Exo.Rp.Core.Events

{
    internal class EnvironmentEvents : IScript
    {
        [ClientEvent("onTownHallInteraction")]
        public void OnVehicleShopInteraction(IPlayer player)
        {
            var townHall = (Cityhall) player.GetCharacter().GetInteractionData().SourceObject;
            townHall.OnInteract(player);
        }

        [ClientEvent("setCharacterName")]
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

        [ClientEvent("CarRent:OnPedInteract")]
        public void OnColshapeHit(IPlayer player)
        {
            player.Emit("CarRent:OpenUI");
        }

        [ClientEvent("CarRent:RentVehicle")]
        public void RentVehicle(IPlayer player, string vehicle, int rentalGroupId)
        {
            CarRent.rentalGroups[rentalGroupId].RentVehicle(player, vehicle);
        }

    }
}