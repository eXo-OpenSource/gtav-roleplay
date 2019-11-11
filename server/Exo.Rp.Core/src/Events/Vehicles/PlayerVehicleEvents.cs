using System;
using System.Collections.Generic;
using AltV.Net;
using AltV.Net.Elements.Entities;
using models.Vehicle;
using server.Vehicles;
using server.Vehicles.Types;
using IPlayer = server.Players.IPlayer;

namespace server.Events.Vehicles
{
    internal class PlayerVehicleEvents : IScript
    {
        [Event("vehicle:GetPlayerVehicles")]
        public void GetPlayerVehicles(IPlayer client)
        {
            var vehicles = new List<VehicleDto>();
            foreach (var veh in Core.GetService<VehicleManager>().GetPlayerVehicles(client))
            {
                vehicles.Add(new VehicleDto()
                {
                    Id = veh.Id,
                    ModelName = "Unbekannt",
                    OwnerId = veh.OwnerId,
                    OwnerName = veh.ownerName,
                    OwnerType = veh.OwnerType,
                    Vehicle = veh.handle.Id
                });
            }
            client.Emit("vehicle:ReceivePlayerVehicles", vehicles);
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<PlayerVehicle>(networkVehicle);
            if (vehicle == null) return;
            if (vehicle.CanEnter(client, seat)) vehicle.OnEnter(client, seat);
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicleAttempt(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<PlayerVehicle>(networkVehicle);
            if (vehicle == null) return;
            if (vehicle.CanEnter(client, seat))
            {
                vehicle.OnStartEnter(client, seat);
            }
            else
            {
                //NAPI.Player.ClearPlayerTasks(client);
                client.SendError("Du darfst dieses Fahrzeug nicht benutzen");
            }
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerExitVehicle(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<PlayerVehicle>(networkVehicle);
            vehicle?.OnExit(client);
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerExitVehicleAttempt(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<PlayerVehicle>(networkVehicle);
            vehicle?.OnStartExit(client);
        }
    }
}