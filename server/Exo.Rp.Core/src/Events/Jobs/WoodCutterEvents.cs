using AltV.Net;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Jobs.Jobs;
using Exo.Rp.Models.Enums;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events.Jobs
{
    internal class WoodCutterEvents : IScript
    {
        [ClientEvent("JobWoodCutter:onTreeInteract")]
        public void OnTreeInteract(IPlayer player)
        {
            var tree = (Wood)player.GetCharacter().GetInteractionData().SourceObject;
            var job = player.GetCharacter().GetJob();
            if (job.JobId != (int)JobId.WoodCutter) return;
            var woodCutterJob = (WoodCutter)job;
            woodCutterJob.OnTreeInteract(player, tree);
        }
    }
}
