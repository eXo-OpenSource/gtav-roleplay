using System.Collections.Generic;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;

namespace server.Environment
{
    internal class IplManager
    {
        private static readonly List<string> Loaded;
        private static readonly List<string> Unloaded;

        static IplManager()
        {
            Loaded = new List<string>
            {
                "FIBlobby"
            };

            Unloaded = new List<string>
            {
                "FIBlobbyfake",
                "bh1_16_refurb",
                "jewel2fake",
                "fakeint",
                "farm_burnt",
                "farm_burnt_props",
                "farmint_cap",
                "facelobbyfake"
            };
        }

        public static void SendToIPlayer(IPlayer player)
        {
            player.Emit("loadIPL", JsonConvert.SerializeObject(Loaded));
            player.Emit("unloadIPL", JsonConvert.SerializeObject(Unloaded));
        }
    }
}