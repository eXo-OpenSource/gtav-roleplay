using System.Collections.Generic;
using server.Players;
using IPlayer = server.Players.IPlayer;

namespace server.World
{
	internal class DoorManager : IManager
	{
		internal class DoorState
		{
			public int Hash { get; set; }
			public bool State { get; set; }
		}

		private readonly PlayerManager _playerManager;

		public DoorManager(PlayerManager playerManager)
		{
			_playerManager = playerManager;
		}

		public void SetDoorState(int hash, bool state)
		{
			var states = new List<DoorState>
			{
				new DoorState
				{
					Hash = hash,
					State = state
				}
			};

			SetDoorStates(states);
		}

		public void SetDoorState(IPlayer player, int hash, bool state)
		{
			var states = new List<DoorState>
			{
				new DoorState
				{
					Hash = hash,
					State = state
				}
			};

			SetDoorStates(player, states);
		}

		public void SetDoorState(IEnumerable<IPlayer> players, int hash, bool state)
		{
			var states = new List<DoorState>
			{
				new DoorState
				{
					Hash = hash,
					State = state
				}
			};

			SetDoorStates(players, states);
		}

		public void SetDoorStates(IEnumerable<DoorState> doorStates)
		{
			foreach (var player in _playerManager.GetLoggedInPlayers())
			{
				player.Emit("DoorManager:setDoorStates", doorStates);
			}
		}

		public void SetDoorStates(IPlayer player, IEnumerable<DoorState> doorStates)
		{
			player.Emit("DoorManager:setDoorStates", doorStates);
		}

		public void SetDoorStates(IEnumerable<IPlayer> players, IEnumerable<DoorState> doorStates)
		{
			foreach (var player in players)
			{
				player.Emit("DoorManager:setDoorStates", doorStates);
			}
		}
	}
}
