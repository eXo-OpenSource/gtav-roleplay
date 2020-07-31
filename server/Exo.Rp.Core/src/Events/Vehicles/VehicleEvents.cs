using System;
using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using server.Util;
using server.Vehicles;
using IPlayer = server.Players.IPlayer;
using Vehicle = server.Vehicles.Vehicle;

namespace server.Events.Vehicles
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

        [Event("Vehicle:ToggleEngine")]
        public void OnVehicleEngineSwitchBind(IPlayer client)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(client.Vehicle);
            if (vehicle == null) return;
            if (client.Vehicle.EngineOn == false)
            {
                if (vehicle.CanStartEngine(client)) vehicle.ToggleEngine(true);
            }
            else
            {
                vehicle.ToggleEngine(false);
            }
        }

        [Event("Vehicle:ToggleLight")]
        public void OnVehicleLightSwitch(IPlayer client)
        {
			var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(client.Vehicle);

			if (vehicle == null) return;
			if (vehicle.CanEnter(client, 0))
			{
				vehicle.ToggleLight();
			}
        }

        [Event("Vehicle:ToggleLock")]
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

        [Event("Vehicle:ToggleDoor")]
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

		[Event("Vehicle:GetInfo")]
		public void GetVehicleInfos(IPlayer client, IVehicle networkVehicle)
		{
			var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<Vehicle>(networkVehicle);
			client.SendChatMessage($"Fahrzeuginformationen von {vehicle.Model}");
			client.SendChatMessage($"Kennzeichen: {vehicle.Plate}");
			client.SendChatMessage($"Zustand: {(vehicle.Locked ? "Verschlossen" : "Geöffnet")}");

		}
	}
}
