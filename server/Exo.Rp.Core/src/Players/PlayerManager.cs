using System.Collections.Generic;
using System.Linq;
using AltV.Net;
using AltV.Net.Enums;
using server.Database;
using server.Extensions;
using server.Translation;
using server.Util.Log;

namespace server.Players
{
    internal class PlayerManager : IManager
    {
        private static readonly Logger<PlayerManager> Logger = new Logger<PlayerManager>();

        private readonly DatabaseContext _databaseContext;
        private readonly Dictionary<int, IPlayer> _players;

        public PlayerManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;

            _players = new Dictionary<int, IPlayer>();
        }

        public IEnumerable<IPlayer> GetLoggedInPlayers()
        {
	        return Alt.GetAllPlayers()
		        .Select(p => p as IPlayer)
		        .Where(p => p?.GetCharacter()?.IsLoggedIn == true);
        }

        public IPlayer GetClient(int accountId)
        {
            return _players.TryGetValue(accountId, out var client) ? client : null;
        }

        public string GetNameFromId(int accountId)
        {
            return _databaseContext.CharacterModel.Local.FirstOrDefault(x => x.Id == accountId)?.FullName ?? "Unbekannt";
        }

        public bool IsPlayerOnline(int accountId)
        {
            return _players.ContainsKey(accountId);
        }
        
        public void OnDisconnect(IPlayer player)
        {
            //TODO Implement that back in
            /*if (player.GetCharacter()?.IsJobActive() == true) player.GetCharacter()?.GetJob()?.StopJob(player);
            Logger.Info("Saved Data for " + player.Name);
            player.GetCharacter()?.Logout();*/
            _players.Remove(player.GetAccount().Id);
        }
        
        public void DoLogin(IPlayer player)
        {
            if (_databaseContext.AccountModel.Local.Count(account => account.HardwareId == player.HardwareIdHash) > 1)
            {
                player.Emit("registerLogin:Error", T._("Es wurden mehrere Accounts mit deiner HardwareId gefunden!", player));
                return;
            }

            if (player.GetAccount() == default || player.GetCharacter() == default)
            {
                player.Emit("registerLogin:Error", T._("Account / Character wurde nicht gefunden.", player));
                return;
            }

            _players.Add(player.GetAccount().Id, player);
            player.GetCharacter().Login(player);
            player.Emit("afterLogin");

			player.SendWarning("Hi");
			player.SendWarning("Test");
		}

		public void PlayerReady(IPlayer player)
        {
            player.SendInitialSync();
        }
    }
}
