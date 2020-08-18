using System;
using AltV.Net;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Vehicles.Types
{
    public partial class TemporaryVehicle : Vehicle
    {
        private static readonly ILogger<TemporaryVehicle> Logger = new Logger<TemporaryVehicle>();

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