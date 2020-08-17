using System;
using AltV.Net;
using server.Updateable;
using server.Util.Log;
using IPlayer = server.Players.IPlayer;

namespace server.Vehicles.Types
{
	public class RentVehicle : Vehicle
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
			return true;
		}

		public override bool CanStartEngine(IPlayer client)
		{
			//return _owner.Equals(client.GetCharacter().GetTeam());
			return true;
		}
	}
}
