using AltV.Net;
using AltV.Net.Elements.Entities;
using server.Players;
using server.Teams;
using IPlayer = server.Players.IPlayer;

namespace server.Vehicles.Types
{
    internal class TeamVehicle : Vehicle
    {
        public override void OnEnter(IPlayer client, int seat)
        {
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
            if (seat != 0) return true;

            return client.GetCharacter().GetTeams() != null && client.GetCharacter().GetTeams().Contains(Core.GetService<TeamManager>().Teams.Find(x => x.Id == OwnerId));
        }

        public override bool CanStartEngine(IPlayer client)
        {
            //return _owner.Equals(client.GetCharacter().GetTeam());
            return true;
        }
    }
}