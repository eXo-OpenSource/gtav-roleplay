using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Data;
using Exo.Rp.Core.Jobs.Jobs;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events.Jobs
{
    internal class PizzaEvents : IScript
    {
        [ClientEvent("JobPizza:StartMission")]
        public void StartMission(IPlayer player)
        {
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int)JobId.Pizzaboy) return;
            var pizzaJob = (PizzaDelivery)job;

            player.GetCharacter().HideInteraction();
            player.SendInformation("Warten auf den Chef...");
            player.SetSyncedMetaData("JobPizza:TakePizza", true);
            player.Position = new Position(-1526.244140625f, -911.1417236328125f, 10.169964790344238f);
            player.PlayAnimation("anim@heists@box_carry@", "idle", (int)AnimationFlags.AllowPlayerControl);
            Task.Delay(5000).ContinueWith(_ => {
                player.SendInformation("Neuer Auftrag - fahr zum Kunden!");
                pizzaJob.CreateRandomDelivery(player);
                player.SetSyncedMetaData("JobPizza:GivePizza", true);
            });
        }
    }
}