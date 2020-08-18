using System.Collections.Generic;
using Exo.Rp.Core.Players;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.World
{
    internal class IplManager : IManager
    {
        private static readonly string[] defaulLoadedtIpls =
        {
            "FIBlobby",
            "canyonriver01_traincrash", "railing_end",
            "hei_carrier", "hei_carrier_DistantLights", "hei_Carrier_int1", "hei_Carrier_int2", "hei_Carrier_int3", "hei_Carrier_int4", "hei_Carrier_int5", "hei_Carrier_int6", "hei_carrier_LODLights",
            "vw_casino_main", "vw_casino_garage", "vw_casino_carpark",
            "facelobby", "facelobby_lod"
        };

        private static readonly string[] defaulUnLoadedtIpls =
        {
            "FIBlobbyfake",
            "bh1_16_refurb",
            "jewel2fake",
            "fakeint",
            "farm_burnt", "farm_burnt_props", "farmint_cap",
            "facelobbyfake"
        };

        private readonly PlayerManager _playerManager;

        public IplManager(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void LoadDefaultIpls(IPlayer player)
        {
            RequestIpl(player, defaulLoadedtIpls);
            RemoveIpl(player, defaulUnLoadedtIpls);
        }

        public void RequestIpl(IList<string> iplList)
        {
            foreach (var player in _playerManager.GetLoggedInPlayers())
            {
                player.Emit("IPLManager:requestIPL", iplList);
            }
        }

        public void RequestIpl(IPlayer player, IEnumerable<string> iplList)
        {
            player.Emit("IPLManager:requestIPL", iplList);
        }

        public void RequestIpl(IEnumerable<IPlayer> players, IEnumerable<string> iplList)
        {
            foreach (var player in players)
            {
                player.Emit("IPLManager:requestIPL", iplList);
            }
        }

        public void RemoveIpl(IEnumerable<string> iplList)
        {
            foreach (var player in _playerManager.GetLoggedInPlayers())
            {
                player.Emit("IPLManager:requestIPL", iplList);
            }
        }

        public void RemoveIpl(IPlayer player, IEnumerable<string> iplList)
        {
            player.Emit("IPLManager:requestIPL", iplList);
        }

        public void RemoveIpl(IEnumerable<IPlayer> players, IEnumerable<string> iplList)
        {
            foreach (var player in players)
            {
                player.Emit("IPLManager:requestIPL", iplList);
            }
        }
    }
}