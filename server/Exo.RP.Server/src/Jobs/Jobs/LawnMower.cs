using System;
using AltV.Net.Elements.Entities;

namespace server.Jobs.Jobs
{
    public class LawnMower
    {
        public Object LawnMowerObject { get; set; }
        public IPlayer Player { get; set; }
        public int Capacity { get; set; }
        public int MaxCapacity { get; set; }
        public int Rtb { get; set; }

        public void Destroy()
        {
            /*if (NAPI.Entity.DoesEntityExist(LawnMowerObject)) LawnMowerObject.Delete();
            try
            {
                Player.ResetData("LawnMower");
            }
            catch
            {
                //
            }*/
        }

        public void DoRtb()
        {
            Capacity = 0;
            MaxCapacity++;
            Rtb++;
        }
    }
}
