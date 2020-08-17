using AltV.Net;
using server.Jobs.Jobs;
using IPlayer = server.Players.IPlayer;

namespace server.Events.Jobs
{
	internal class LawnMowerEvents : IScript
	{
		[ClientEvent("JobLawn:OnMarkerHit")]
		public void OnMarkerHit(IPlayer client)
		{
			var job = client.GetCharacter().GetJob() as LawnCaretaker;
			job?.OnMarkerColEnter(client);
		}
	}
}
