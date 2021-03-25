using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Sdk.Logger;
using System.Timers;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Vehicles
{
    public partial class Vehicle
    {
        private static readonly ILogger<Vehicle> Logger = new Logger<Vehicle>();

        public IVehicle handle;
        private bool _lightStatus;
        public string ownerName;
        private Timer fuelTimer;

        public virtual IVehicle Spawn()
        {
            handle = Alt.CreateVehicle(Model, Pos, new Rotation(0f, 0f, RotZ));
            handle.ManualEngineControl = true;
            handle.NumberplateText = Plate;
            handle.SetNumberPlateStyle(NumberPlateStyle.YellowBlue);
            handle.PrimaryColorRgb = new Rgba((byte) (Color1 >> 24), (byte) (Color1 >> 16), (byte) (Color1 >> 8), (byte)Color1);
            handle.SecondaryColorRgb = new Rgba((byte) (Color2 >> 24), (byte) (Color2 >> 16), (byte) (Color2 >> 8), (byte)Color2);
            handle.SetSyncedMetaData("OwnerType", (int)OwnerType);
            handle.SetSyncedMetaData("OwnerId", OwnerId);
            return handle;
        }

        public void Save()
        {
            Logger.Info("Saved vehicle " + Id);
            Pos = handle.Position;
            RotZ = handle.Rotation.Yaw;
        }

        public virtual void OnEnter(IPlayer client, int seat)
        {
            if (seat == 1)
            {
                client.ShowRadar(true);
                client.SendInformation("Druecke ~b~X~w~ um mit dem Fahrzeug zu interagieren!");
                handle.GetSyncedMetaData("vehicle.Engine", out bool engineStatus);
                handle.EngineOn = engineStatus;
                handle.SetStreamSyncedMetaData("vehicle.Light", _lightStatus);
                handle.SetSyncedMetaData("vehicle.Engine", engineStatus);
            }
            Alt.EmitAllClients("onIPlayerVehicleEnter", client, handle, seat);
        }

        public virtual void OnStartEnter(IPlayer client, int seat)
        {
            Alt.EmitAllClients("onIPlayerVehicleStartEnter", client, handle, seat);
            Alt.EmitAllClients("syncVehicleEngine", handle, handle.EngineOn);
        }

        public virtual void OnExit(IPlayer client)
        {
            client.ShowRadar(false);
            Alt.EmitAllClients("onIPlayerVehicleExit", client, handle);
        }

        public virtual void OnStartExit(IPlayer client)
        {
            Alt.EmitAllClients("onIPlayerVehicleStartExit", client, handle);
        }

        public virtual bool CanEnter(IPlayer client, int seat)
        {
            return true;
        }

        public virtual void ToggleEngine(IPlayer client, bool state)
        {
            handle.EngineOn = state;
            handle.SetSyncedMetaData("vehicle.Engine", state);

            if (handle.EngineOn)
            {
                fuelTimer = new Timer(5000);
                fuelTimer.Elapsed += (args, e) =>
                {
                    if (Fuel <= 0f) {
                        handle.EngineOn = false;
                        fuelTimer.Enabled = false;
                        return;
                    }
                    Fuel--;
                };
                fuelTimer.Enabled = true;
            } else
            {
                fuelTimer.Enabled = false;
            }
        }

        public virtual void ToggleLight()
        {
            _lightStatus = !_lightStatus;
            handle.SetStreamSyncedMetaData("vehicle.Light", _lightStatus);
        }

        public virtual void ToggleSeatbelt(IPlayer client, bool state)
        {
            /*client.Seatbelt = state;
            handle.SetSyncedMetaData("vehicle.Seatbelt", client.Seatbelt);*/
        }

        public virtual bool CanStartEngine(IPlayer client)
        {
            if (Fuel <= 0f && FuelType != Models.Enums.FuelType.None)
            {
                client.SendError("Der Tank ist leer.");
                return false;
            } else
            {
                return true;
            }
        }

        public virtual void ToggleLocked(IPlayer client, bool? state = null)
        {
            var newState = state ?? (handle.LockState != VehicleLockState.Locked);
            handle.LockState = newState ? VehicleLockState.Locked : VehicleLockState.Unlocked;
        }

        public virtual void ToggleVehicleDoor(IPlayer player, byte door, bool open)
        {
            if (handle.LockState == VehicleLockState.Locked)
            {
                player.SendError("Dieses Fahrzeug ist abgeschlossen!");
                return;;
            }

            if (door == (byte)VehicleDoor.Hood)
            {
                handle.SetDoorState(door, (open ? (byte)VehicleDoorState.OpenedLevel4 : (byte)VehicleDoorState.Closed));
                handle.SetStreamSyncedMetaData("vehicle.EngineHood", open);
                player.SendInformation($"Motorhaube erfolgreich {(open ? "geöffnet" : "geschlossen")}!");
            }
            else
            {
                handle.SetDoorState(door, (open ? (byte)VehicleDoorState.OpenedLevel5 : (byte)VehicleDoorState.Closed));
                handle.SetStreamSyncedMetaData("vehicle.Trunk", open);
                player.SendInformation($"Kofferraum erfolgreich {(open ? "geöffnet" : "geschlossen")}!");
            }
        }
    }
}