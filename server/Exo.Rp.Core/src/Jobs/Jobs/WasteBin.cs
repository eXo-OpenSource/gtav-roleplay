using Exo.Rp.Core.Streamer;
using Exo.Rp.Core.Streamer.Entities;

namespace Exo.Rp.Core.Jobs.Jobs
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
            Core.GetService<PublicStreamer>().RemoveObject(BinObject);
            Full = false;
        }
    }
}