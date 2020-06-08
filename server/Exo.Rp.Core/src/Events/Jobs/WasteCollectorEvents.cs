using AltV.Net;
using AltV.Net.Elements.Entities;
using models.Enums;
using server.Extensions;
using server.Jobs;
using server.Jobs.Jobs;
using IPlayer = server.Players.IPlayer;

namespace server.Events.Jobs
{
    internal class WasteCollectorEvents : IScript
    {
        [Event("JobTrash:onVehicleMarkerHit")]
        public void OnVehicleMarkerHit(IPlayer player, IVehicle vehicle)
        {
	        var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.WasteCollector) return;
            var wasteJob = (WasteCollector) job;
            wasteJob.OnVehicleMarkerHit(player, vehicle);
        }

        [Event("JobTrash:OnWasteBinPlaced")]
        public void OnWasteBinPlaced(IPlayer player, string pos, string rot)
        {
            var job = (WasteCollector) Core.GetService<JobManager>().GetJob(1);
            if (job.JobId == (int) JobId.WasteCollector)
                job.AddWastebin(player, pos.DeserializeVector(), rot.DeserializeVector());
        }
    }
}
