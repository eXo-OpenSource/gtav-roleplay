using AltV.Net;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Vehicles;
using Exo.Rp.Core.Vehicles.Types;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Events.Vehicles
{
    internal class TeamVehicleEvents : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<TeamVehicle>(networkVehicle);
            if (vehicle == null) return;
            if (vehicle.CanEnter(client, seat)) vehicle.OnEnter(client, seat);
        }

        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicleAttempt(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<TeamVehicle>(networkVehicle);
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
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<TeamVehicle>(networkVehicle);
            vehicle?.OnExit(client);
        }

        [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
        public void OnPlayerExitVehicleAttempt(IVehicle networkVehicle, IPlayer client, byte seat)
        {
            var vehicle = Core.GetService<VehicleManager>().GetVehicleFromHandle<TeamVehicle>(networkVehicle);
            vehicle?.OnStartExit(client);
        }
    }
}