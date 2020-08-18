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
        public void OnEnterDeliveryMarker(IPlayer player, ColShape shape)
        {
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.Farmer) return;
            var farmerJob = (Farmer) job;
            farmerJob.OnDeliveryMarkerHit(player);
        }
    }
}