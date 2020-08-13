using AltV.Net;
using AltV.Net.Elements.Entities;
using models.Enums;
using server.Jobs.Jobs;
using IPlayer = server.Players.IPlayer;

namespace server.Events.Jobs
{
    internal class FarmerEvents : IScript
    {
        [Event("JobFarmer:onTreeInteract")]
        public void OnTreeInteract(IPlayer player)
        {
            var tree = (Tree) player.GetCharacter().GetInteractionData().SourceObject;
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.Farmer) return;
            var farmerJob = (Farmer) job;
            farmerJob.OnTreeInteract(player, tree);
        }

        [Event("JobFarmer:onEnterDeliveryMarker")]
        public void OnEnterDeliveryMarker(IPlayer player, ColShape shape)
        {
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.Farmer) return;
            var farmerJob = (Farmer) job;
            farmerJob.OnDeliveryMarkerHit(player);
        }
    }
}
