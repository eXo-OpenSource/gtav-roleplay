using AltV.Net;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Jobs.Jobs;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events.Jobs
{
    internal class FarmerEvents : IScript
    {
        [ClientEvent("JobFarmer:onTreeInteract")]
        public void OnTreeInteract(IPlayer player)
        {
            var tree = (Tree) player.GetCharacter().GetInteractionData().SourceObject;
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.Farmer) return;
            var farmerJob = (Farmer) job;
            farmerJob.OnTreeInteract(player, tree);
        }

        [ClientEvent("JobFarmer:onEnterDeliveryMarker")]
        public void OnEnterDeliveryMarker(IPlayer player)
        {
            Alt.Log("1");
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.Farmer) return;
            var farmerJob = (Farmer) job;
            Alt.Log("2");
            int itemCount = player.GetCharacter().GetInventory().GetItemCount(farmerJob.apple);

            if (player.GetCharacter().GetInventory().GetItemCount(farmerJob.apple) > 0)
            {
                player.SendSuccess($"Du hast {itemCount} Äpfel abgegeben.");
                player.GetCharacter().GetInventory().RemoveItem(farmerJob.apple, itemCount);
            }
            else
            {
                player.SendInformation("todo inventory bag");
                player.SendError("Du hast keine Äpfel zum Abgeben.");
            }
        }

        [ClientEvent("JobFarmer:Start")]
        public void StartFarmerJob(IPlayer player, int jobId)
        {
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.Farmer) return;
            var farmerJob = (Farmer) job;
            farmerJob.StartJobType(player, jobId);
            player.SetData("FarmerJob:JobType", jobId);
        }
    }
}