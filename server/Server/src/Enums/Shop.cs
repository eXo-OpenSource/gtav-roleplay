using System;
using System.Collections.Generic;

namespace server.Enums
{
    // TODO: Need to be moved out of enums namespace
    [Serializable]
    public class VehicleShopOptions
    {
        public float SpawnPosX { get; set; }
        public float SpawnPosY { get; set; }
        public float SpawnPosZ { get; set; }
        public float SpawnRotZ { get; set; }
    }

    public class ItemShopOptions
    {
        public Dictionary<int, int> Items { get; set; }
    }

    public class TuningShopOptions
    {
        public float SpawnPosX { get; set; }
        public float SpawnPosY { get; set; }
        public float SpawnPosZ { get; set; }
        public float SpawnRotZ { get; set; }
    }
}