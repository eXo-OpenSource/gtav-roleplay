using AltV.Net;
using models.Enums;
using server.Players;
using IPlayer = server.Players.IPlayer;

namespace server.Vehicles.Types
{
    public class PlayerVehicle : Vehicle
    {
        private IPlayer _owner;
        public IPlayer Owner => _owner ??= OwnerType == OwnerType.Player ? Core.GetService<PlayerManager>().GetClient(OwnerId) : null;

        public override void OnEnter(IPlayer client, int seat)
        {
            base.OnEnter(client, seat);
            Alt.EmitAllClients("onIPlayerPlayerVehicleEnter", client, handle, seat);
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
            return OwnerId == client.GetId();
        }
    }
}