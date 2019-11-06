using System.Collections.Generic;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;

namespace server.Environment
{
    internal class IplManager : IManager
    {
        private readonly List<string> Loaded;
        private readonly List<string> Unloaded;

        public IplManager()
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

        public void SendToIPlayer(IPlayer player)
        {
            player.Emit("loadIPL", JsonConvert.SerializeObject(Loaded));
            player.Emit("unloadIPL", JsonConvert.SerializeObject(Unloaded));
        }
    }
}