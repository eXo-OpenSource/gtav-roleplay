using AltV.Net;
using IPlayer = Exo.Rp.Core.Players.IPlayer;
using Exo.Rp.Core.Environment.CarRentTypes;
using AltV.Net.Data;

namespace Exo.Rp.Core.Vehicles.Types
{
    public class RentVehicle : Vehicle
    {
        public RentalGroup RentalGroup;
        public bool IsActive;
        public float RespawnPosX { get; set; }
        public float RespawnPosY { get; set; }
        public float RespawnPosZ { get; set; }
        public float RespawnRotZ { get; set; }

        public Position RespawnPos
        {
            get => new Position(RespawnPosX, RespawnPosY, RespawnPosZ);
            set
            {
                RespawnPosX = value.X;
                RespawnPosY = value.Y;
                RespawnPosZ = value.Z;
            }
        }
        public void Respawn()
        {
            OwnerId = -1;
            IsActive = false;
            handle.BodyHealth = 100;
            handle.Rotation = new Rotation(0, 0, RespawnRotZ);
            handle.Position = RespawnPos;
        }
        public override void OnEnter(IPlayer client, int seat)
        {
            if (IsActive) client.Emit("CarRent:DeleteRentMarker");
            base.OnEnter(client, seat);
            Alt.EmitAllClients("onIPlayerTeamVehicleEnter", client, handle, seat);
        }

        public override void OnStartEnter(IPlayer client, int seat)
        {
            base.OnStartEnter(client, seat);
            Alt.EmitAllClients("onIPlayerTeamVehicleStartEnter", client, handle, seat);
        }

        public override void OnExit(IPlayer client)
        {
            base.OnExit(client);
            Alt.EmitAllClients("onIPlayerTeamVehicleExit", client, handle);
        }

        public override void OnStartExit(IPlayer client)
        {
            base.OnStartExit(client);
            Alt.EmitAllClients("onIPlayerTeamVehicleStartExit", client, handle);
        }

        public override bool CanEnter(IPlayer client, int seat)
        {
            return OwnerId == client.GetId();
        }

        public override bool CanStartEngine(IPlayer client)
        {
            //return _owner.Equals(client.GetCharacter().GetTeam());
            //return OwnerId == client.GetId();
            return true;
        }
    }
}