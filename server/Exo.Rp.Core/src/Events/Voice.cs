using AltV.Net;
using server.Players;

namespace server.Events
{
    internal class Voice : IScript
    {
        [ClientEvent("Voice:AddListener")]
        public void AddListener(IPlayer player, IPlayer target)
        {
            if (target == null) return;
            //player.EnableVoiceTo(target);
        }

        [ClientEvent("Voice:RemoveListener")]
        public void RemoveListener(IPlayer player, IPlayer target)
        {
            if (target == null) return;
            // player.DisableVoiceTo(target);
        }
    }
}