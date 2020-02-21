using System;
using AltV.Net;
using server.Updateable;
using server.Util.Log;
using IPlayer = server.Players.IPlayer;

namespace server.Vehicles.Types
{
    public class TemporaryVehicle : Vehicle
    {
        private static readonly Logger<TemporaryVehicle> Logger = new Logger<TemporaryVehicle>();

        public DateTime LastUsed = DateTime.UtcNow;

        public override void OnEnter(IPlayer client, int seat)
        {
            base.OnEnter(client, seat);
            Alt.EmitAllClients("onIPlayerPlayerVehicleEnter", client, handle, seat);

            LastUsed = DateTime.UtcNow;
        }

        public override void OnStartEnter(IPlayer client, int seat)
        {
            base.OnStartEnter(client, seat);
            Alt.EmitAllClients("onIPlayerPlayerVehicleStartEnter", client, handle, seat);
        }

        public override void OnExit(IPlayer client)
        {
            base.OnExit(client);
            Alt.EmitAllClients("onIPlayerPlayerVehicleExit", client, handle);
        }

        public override void OnStartExit(IPlayer client)
        {
            base.OnStartExit(client);
            Alt.EmitAllClients("onIPlayerPlayerVehicleStartExit", client, handle);
        }

        public override bool CanEnter(IPlayer client, int seat)
        {
            return true;
        }
    }
}