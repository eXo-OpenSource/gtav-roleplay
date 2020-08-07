using AltV.Net;
using AltV.Net.Elements.Entities;
using models.Enums;
using server.Jobs.Jobs;
using IPlayer = server.Players.IPlayer;

namespace server.Events.Jobs
{
	internal class LawnMowerEvents : IScript
	{
		[ClientEvent("JobLawn:OnMarkerHit")]
		public void OnMarkerHit(IPlayer client)
		{
			var job = client.GetCharacter().GetJob();
			if (job.JobId != (int)JobId.LawnCaretaker) return;

			var lawnJob = (LawnCaretaker)job;
			lawnJob.OnMarkerColEnter(client);
		}
	}
}
