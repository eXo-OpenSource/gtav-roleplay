using AltV.Net;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Extensions;
using Exo.Rp.Core.Jobs;
using Exo.Rp.Core.Jobs.Jobs;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events.Jobs
{
    internal class WasteCollectorEvents : IScript
    {
        [ClientEvent("JobTrash:onVehicleMarkerHit")]
        public void OnVehicleMarkerHit(IPlayer player, IVehicle vehicle)
        {
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int) JobId.WasteCollector) return;
            var wasteJob = (WasteCollector) job;
            wasteJob.OnVehicleMarkerHit(player, vehicle);
        }

        [ClientEvent("JobTrash:OnWasteBinPlaced")]
        public void OnWasteBinPlaced(IPlayer player, string pos, string rot)
        {
            var job = (WasteCollector) Core.GetService<JobManager>().GetJob(1);
            if (job.JobId == (int) JobId.WasteCollector)
                job.AddWastebin(player, pos.DeserializeVector(), rot.DeserializeVector());
        }
    }
}