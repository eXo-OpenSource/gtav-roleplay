using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Util;
using Exo.Rp.Core.Vehicles;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Vehicle = Exo.Rp.Core.Vehicles.Vehicle;

namespace Exo.Rp.Core.Events.Vehicles
{
    internal class VehicleEvents : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            if (vehicle == null) return;
            if (vehicle.CanEnter(client, seat)) vehicle.OnEnter(client, seat);
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicleAttempt(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            if (vehicle != null)
            {
                if (vehicle.CanEnter(client, seat))
                {
                    vehicle.OnStartEnter(client, seat);
                }
                else
                {
                    // NAPI.Player.ClearPlayerTasks(client);
                    client.SendError("Du darfst dieses Fahrzeug nicht benutzen");
                }
            }
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerExitVehicle(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            vehicle?.OnExit(client);
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerExitVehicleAttempt(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            vehicle?.OnStartExit(client);
        }

        [ClientEvent("Vehicle:ToggleEngine")]
        public void OnVehicleEngineSwitchBind(IPlayer client)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(client.Vehicle);
            if (vehicle == null) return;
            if (client.Vehicle.EngineOn == false)
            {
                if (vehicle.CanStartEngine(client)) vehicle.ToggleEngine(client, true);
            }
            else
            {
                vehicle.ToggleEngine(client, false);
            }
        }

        [ClientEvent("Vehicle:ToggleLight")]
        public void OnVehicleLightSwitch(IPlayer client)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(client.Vehicle);
            if (vehicle == null) return;

            if (vehicle.CanEnter(client, 0))
            {
                vehicle.ToggleLight();
            }
        }

        [ClientEvent("Vehicle:ToggleLock")]
        public void OnVehicleLockSwitch(IPlayer client, IVehicle networkVehicle)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            if (vehicle == null) return;

            if (vehicle.CanEnter(client, 0))
            {
                vehicle.ToggleLocked(client);
                client.SendInformation($"Du hast das Fahrzeug {(vehicle.handle.LockState == VehicleLockState.Locked ? "zugesperrt" : "aufgesperrt")}!");
            }
            else
            {
                client.SendError("Du hast keinen Schluessel fuer dieses Fahrzeug!");
            }
        }

        [ClientEvent("Vehicle:ToggleDoor")]
        public void ToggleVehicleDoor(IPlayer client, IVehicle networkVehicle, int door, bool open)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            if (vehicle != null)
            {
                vehicle.ToggleVehicleDoor(client, (byte)door, open);
            }
            else
            {
                networkVehicle.ToggleDoor((byte)door);
            }
        }

        [ClientEvent("vehicle:OpenVehicleMenu")]
        public void OpenVehicleMenu(IPlayer client, IVehicle veh)
        {
            if (veh != null)
            {
                client.SendChatMessage(null, veh.Model.ToString());
                //client.Emit("vehicle:OpenInteractionMenu", veh);
            }
            else
            {
                client.SendChatMessage(null, "Vehicle not found! ");
            }
        }

        [ClientEvent("Vehicle:GetInfo")]
        public void GetVehicleInfos(IPlayer client, IVehicle networkVehicle)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);

            client.SendInformation($"Fahrzeug: {vehicle.Model}");
            client.SendInformation($"Kennzeichen: {vehicle.Plate}");
            client.SendInformation($"Zustand: {(vehicle.Locked ? "Verschlossen" : "Geöffnet")}");
        }
    }
}