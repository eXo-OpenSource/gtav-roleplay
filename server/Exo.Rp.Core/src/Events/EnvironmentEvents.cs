using AltV.Net;
using Exo.Rp.Core.Environment;
using Exo.Rp.Core.Players;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Events

{
    internal class EnvironmentEvents : IScript
    {
        [ClientEvent("Cityhall:OnEntranceInteract")]
        public void OnCityhallInteract(IPlayer player)
        {
            var townHall = (Cityhall)player.GetCharacter().GetInteractionData().SourceObject;
            townHall.OnInteract(player);
        }

        [ClientEvent("Drivingschool:OnPedInteract")]
        public void OnPedInteract(IPlayer player)
        {
            player.SendInformation("Die Fahrprüfung kannst Du am Laptop für $5000 starten!");
        }

        [ClientEvent("Drivingschool:OnExamFinished")]
        public void OnExamFinished(IPlayer player, int score)
        {
            if (score >= 80)
            {
                player.GetCharacter().SetPlayerLicense(License.Car, 1);
                player.SendSuccess($"Glückwunsch zur Fahrerlaubnis! Du hast mit {score}% bestanden!");
            }
            else
                player.SendError($"Durchgefallen! Du bist mit {score}% durchgefallen!");
        }

        [ClientEvent("Drivingschool:OnLaptopInteract")]
        public void OnLaptopInteract(IPlayer player)
        {
            if (player.GetCharacter().GetMoney(true) >= 5000)
            {
                player.GetCharacter().TakeMoney(5000, "Drivingschool Exam", true);
                player.SendNotification("Das Geld wurde Dir vom Konto abgebucht.");
                player.Emit("Drivingschool:OpenUi");
            } else
            {
                player.SendError("Die Fahrprüfung kostet $5000!");
            }
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