using System;
using AltV.Net.Elements.Entities;

namespace server.Jobs.Jobs
{
    public class LawnMower
    {
        public Object LawnMowerObject { get; set; }
        public IPlayer Player { get; set; }
        public double Capacity { get; set; }
        public double MaxCapacity { get; set; }
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