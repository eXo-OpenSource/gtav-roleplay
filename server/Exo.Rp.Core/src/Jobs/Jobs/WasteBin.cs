using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Streamer;
using server.Streamer.Entities;
using server.Streamer.Private;
using Object = AltV.Net.Elements.Entities.IWorldObject;

namespace server.Jobs.Jobs
{
    public class WasteBin
    {
        public StreamObject BinObject { get; set; }
        public Colshape.Colshape Col { get; set; }
        public bool Full { get; set; }

        public PrivateEntity Blip { get; set; }

        public void Destroy()
        {
	        if (Col.Exists) Col.Remove();
	        Core.GetService<ObjectStreamer>().Remove(BinObject);
	        Core.GetService<PrivateStreamer>().RemoveEntity(Blip);
            Full = false;
        }
    }
}
