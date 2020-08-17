using System.Collections.Generic;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;

namespace server.Util
{
    public static class Vehicle
    {
        private static readonly Dictionary<IVehicle, Dictionary<int, bool>> DoorOpen = new Dictionary<IVehicle, Dictionary<int, bool>>();
        private static readonly Dictionary<IVehicle, bool> LightOn = new Dictionary<IVehicle, bool>();

        public static void ToggleDoor(this IVehicle vehicle, int door)
        {
            if (!DoorOpen.ContainsKey(vehicle))
            {
                DoorOpen.Add(vehicle, new Dictionary<int, bool>());
            }

            if (!DoorOpen[vehicle].ContainsKey(door))
            {
                DoorOpen[vehicle].Add(door, false);
            }

            DoorOpen[vehicle][door] = !DoorOpen[vehicle][door];
            vehicle.SetSyncedMetaData("vehicle:doorStatus", JsonConvert.SerializeObject(DoorOpen[vehicle]));
        }

        public static void ToggleLight(this IVehicle vehicle)
        {
            if (!LightOn.ContainsKey(vehicle))
            {
                LightOn.Add(vehicle, false);
            }

            LightOn[vehicle] = !LightOn[vehicle];
            vehicle.SetSyncedMetaData("vehicle:lightStatus", LightOn[vehicle]);
        }



    }
}