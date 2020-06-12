using System.Collections.Generic;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using server.Players;
using IPlayer = server.Players.IPlayer;

namespace server.World
{
	internal class IplManager : IManager
	{
		private static readonly string[] defaulLoadedtIpls =
		{
			"FIBlobby",
			"canyonriver01_traincrash", "railing_end"
		};

		private static readonly string[] defaulUnLoadedtIpls =
		{
			"FIBlobbyfake",
			"bh1_16_refurb",
			"jewel2fake",
			"fakeint",
			"farm_burnt", "farm_burnt_props", "farmint_cap",
			"facelobbyfake"
		};

		private readonly PlayerManager _playerManager;

		public IplManager(PlayerManager playerManager)
		{
			_playerManager = playerManager;
		}

		public void LoadDefaultIpls(IPlayer player)
		{
			RequestIpl(player, defaulLoadedtIpls);
			RemoveIpl(player, defaulUnLoadedtIpls);
		}

		public void RequestIpl(IList<string> iplList)
		{
			foreach (var player in _playerManager.GetLoggedInPlayers())
			{
				player.Emit("IPLManager:requestIPL", iplList);
			}
		}

		public void RequestIpl(IPlayer player, IEnumerable<string> iplList)
		{
			player.Emit("IPLManager:requestIPL", iplList);
		}

		public void RequestIpl(IEnumerable<IPlayer> players, IEnumerable<string> iplList)
		{
			foreach (var player in players)
			{
				player.Emit("IPLManager:requestIPL", iplList);
			}
		}

		public void RemoveIpl(IEnumerable<string> iplList)
		{
			foreach (var player in _playerManager.GetLoggedInPlayers())
			{
				player.Emit("IPLManager:requestIPL", iplList);
			}
		}

		public void RemoveIpl(IPlayer player, IEnumerable<string> iplList)
		{
			player.Emit("IPLManager:requestIPL", iplList);
		}

		public void RemoveIpl(IEnumerable<IPlayer> players, IEnumerable<string> iplList)
		{
			foreach (var player in players)
			{
				player.Emit("IPLManager:requestIPL", iplList);
			}
		}
	}
}
