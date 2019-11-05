using AltV.Net.Elements.Entities;
using Object = AltV.Net.Elements.Entities.IWorldObject;

namespace server.Jobs.Jobs
{
    public class WasteBin
    {
        public Object BinObject { get; set; }
        public ColShape Col { get; set; }
        public bool Full { get; set; }

        public void Destroy()
        {
            if (BinObject.Exists) BinObject.OnRemove();
            if (Col.Exists) Col.Remove();
            Full = false;
        }
    }
}
