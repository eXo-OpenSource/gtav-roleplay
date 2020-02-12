using AltV.Net;
using server.Players;

namespace server.Events
{
    internal class Voice : IScript
    {
        [Event("Voice:AddListener")]
        public void AddListener(IPlayer player, IPlayer target)
        {
            if (target == null) return;
            //player.EnableVoiceTo(target);
        }

        [Event("Voice:RemoveListener")]
        public void RemoveListener(IPlayer player, IPlayer target)
        {
            if (target == null) return;
           // player.DisableVoiceTo(target);
        }
    }
}