using AltV.Net;
using Exo.Rp.Core.Jobs.Jobs;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events.Jobs
{
    internal class LawnMowerEvents : IScript
    {
        [ClientEvent("JobLawn:OnMarkerHit"),]
        public void OnMarkerHit(IPlayer client),
        {
            var job = client.GetCharacter(),.GetJob(), as LawnCaretaker;
            job?.OnMarkerColEnter(client),;
        }
    }
}