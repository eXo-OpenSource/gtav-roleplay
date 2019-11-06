using System;
using System.Collections.Generic;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Players.Accounts;
using server.Util.Log;
using Character = server.Players.Characters.Character;

namespace server.Players
{
    internal class PlayerManager : IManager
    {
        private static readonly Logger<PlayerManager> Logger = new Logger<PlayerManager>();

        private readonly Dictionary<int, Interfaces.IPlayer> Players;

        public PlayerManager()
        {
            Players = new Dictionary<int, Interfaces.IPlayer>();
        }

        public Interfaces.IPlayer GetClient(int accountId)
        {
            return Players.TryGetValue(accountId, out var client) ? client : null;
        }

        public string GetName(int accountId)
        {
            return ContextFactory.Instance.CharacterModel.Local.FirstOrDefault(x => x.Id == accountId)?.FullName ?? "Unbekannt";
        }

        public bool IsPlayerOnline(int accountId)
        {
            return Players.ContainsKey(accountId);
        }

        /*
        public void OnDisconnect(IPlayer player)
        {
            if (player.GetCharacter()?.IsJobActive() == true) player.GetCharacter()?.GetJob()?.StopJob(player);
            Logger.Info("Saved Data for " + player.Name);
            player.GetCharacter()?.Logout();
            Players.Remove(player.GetAccountModel().Id);
            Accounts.Remove(player);
            Characters.Remove(player);
        }
        */
        
        public void DoLogin(Interfaces.IPlayer player)
        {
            Players.Add(player.GetAccount().Id, player);
            player.GetCharacter().Login(player);
            player.Emit("afterLogin");
        }

        public bool DoesAccountExist(Interfaces.IPlayer player)
        {
            return ContextFactory.Instance.AccountModel.Local.Any(x => x.SocialClubName == player.SocialClubId.ToString());
        }

        public void PlayerReady(Interfaces.IPlayer player)
        {
            //player.SendInitialSync();
        }
    }
}