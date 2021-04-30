using AltV.Net;
using Exo.Rp.Core.Environment;
using Exo.Rp.Core.Players;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Events

{
    internal class EnvironmentEvents : IScript
    {
        [ClientEvent("Cityhall:OnEntranceInteract")]
        public void OnCityhallEntranceInteract(IPlayer player)
        {
            var townHall = (Cityhall)player.GetCharacter().GetInteractionData().SourceObject;
            townHall.Enter(player);
        }

        [ClientEvent("Cityhall:OnExitInteract")]
        public void OnCityhallExitInteract(IPlayer player)
        {
            var townHall = (Cityhall)player.GetCharacter().GetInteractionData().SourceObject;
            townHall.Leave(player);
        }

        [ClientEvent("Cityhall:OnLicensesPedInteract")]
        public void OnCityhallLicensesPedInteract(IPlayer player)
        {
            var townHall = (Cityhall)player.GetCharacter().GetInteractionData().SourceObject;
            townHall.OnLicensesPedInteract(player);
        }

        [ClientEvent("Drivingschool:OnPedInteract")]
        public void OnPedInteract(IPlayer player)
        {
            player.SendInformation($"Die Fahrprüfung kannst Du am Laptop für ${(int)LicensePrice.Car} starten!");
        }

        [ClientEvent("Drivingschool:OnExamFinished")]
        public void OnExamFinished(IPlayer player, int score, License license)
        {
            var drivingschool = (Drivingschool)player.GetCharacter().GetInteractionData().SourceObject;
            drivingschool.OnExamFinished(player, score, license);
        }

        [ClientEvent("Drivingschool:OnLaptopInteract")]
        public void OnLaptopInteract(IPlayer player)
        {
            var drivingschool = (Drivingschool)player.GetCharacter().GetInteractionData().SourceObject;
            drivingschool.StartDrivingExam(player, License.Car);
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