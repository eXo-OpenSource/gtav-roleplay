﻿using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using server.Util;
using server.Vehicles;
using server.Vehicles.Types;
using IPlayer = server.Players.IPlayer;
using Vehicle = server.Vehicles.Vehicle;

namespace server.Events.Vehicles
{
    internal class VehicleEvents : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(IPlayer client, IVehicle networkVehicle, int seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<server.Vehicles.Vehicle>(networkVehicle);
            if (vehicle == null) return;
            if (vehicle.CanEnter(client, seat)) vehicle.OnEnter(client, seat);
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicleAttempt(IPlayer client, IVehicle networkVehicle, int seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<server.Vehicles.Vehicle>(networkVehicle);
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
        public void OnPlayerExitVehicle(IPlayer client, IVehicle networkVehicle)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<server.Vehicles.Vehicle>(networkVehicle);
            vehicle?.OnExit(client);
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerExitVehicleAttempt(IPlayer client, IVehicle networkVehicle)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<server.Vehicles.Vehicle>(networkVehicle);
            vehicle?.OnStartExit(client);
        }

        [Event("onVehicleEngineSwitchBind")]
        public void OnVehicleEngineSwitchBind(IPlayer client)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<server.Vehicles.Vehicle>(client.Vehicle);
            if (client.Vehicle.EngineOn == false)
            {
                if (vehicle.CanStartEngine(client)) vehicle.ToggleEngine(true);
            }
            else
            {
                vehicle.ToggleEngine(false);
            }
        }

        [Event("onVehicleLockSwitch")]
        public void OnVehicleLockSwitch(IPlayer client, IVehicle networkVehicle, byte state)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<server.Vehicles.Vehicle>(networkVehicle);
            if (vehicle == null) return;
            if (vehicle.CanEnter(client, 0))
            {
                vehicle.ToggleLocked(client);
                client.SendInformation($"Du hast das Fahrzeug {(vehicle.handle.LockState == VehicleLockState.Locked ? "zugesperrt" : "aufgesperrt")}!");
            }
            else
            {
                //NAPI.Player.ClearPlayerTasks(client);
                client.SendError("Du hast keinen Schluessel fuer dieses Fahrzeug!");
            }
        }

        [Event("vehicle:ToggleDoor")]
        public void ToggleVehicleDoor(IPlayer client, IVehicle networkVehicle, byte door)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
            if (vehicle != null)
            {
                vehicle.ToggleVehicleDoor(client, door);
            }
            else
            {
                networkVehicle.ToggleDoor(door);
            }
        }

        [Event("vehicle:OpenVehicleMenu")]
        public void OpenVehicleMenu(IPlayer client, IVehicle veh)
        {
            if (veh != null)
            {
                client.SendChatMessage(veh.Model.ToString());
                //client.Emit("vehicle:OpenInteractionMenu", veh);
            }
            else
            {
                client.SendChatMessage("Vehicle not found! ");
            }
        }
    }
}