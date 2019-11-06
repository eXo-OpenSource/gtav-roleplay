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

        private readonly Dictionary<IPlayer, Account> Accounts;
        private readonly Dictionary<IPlayer, Character> Characters;
        private readonly Dictionary<int, IPlayer> Players;

        public PlayerManager()
        {
            Players = new Dictionary<int, IPlayer>();
            Characters = new Dictionary<IPlayer, Character>();
            Accounts = new Dictionary<IPlayer, Account>();
        }

        public IPlayer GetClient(int accountId)
        {
            return Players.TryGetValue(accountId, out var client) ? client : null;
        }

        public Character GetCharacter(IPlayer player)
        {
            if (player == null) return null;
            return Characters.TryGetValue(player, out var character) ? character : null;
        }

        public Account GetAccount(IPlayer player)
        {
            if (player == null) return null;
            return Accounts.TryGetValue(player, out var account) ? account : null;
        }

        public Account GetAccountBySerial(IPlayer player)
        {
            if (player == null) return null;
            foreach (var (_, accountModel) in Accounts)
            {
                if (accountModel.Serial == player.HardwareIdHash.ToString())
                    return accountModel;
            }

            return null;
        }

        public string GetName(int accountId)
        {
            return ContextFactory.Instance.CharacterModel.Local.FirstOrDefault(x => x.Id == accountId)?.FullName ?? "Unbekannt";
        }

        public bool IsPlayerOnline(int accountId)
        {
            return Players.ContainsKey(accountId);
        }

        public void OnDisconnect(IPlayer player)
        {
            if (player.GetCharacter()?.IsJobActive() == true) player.GetCharacter()?.GetJob()?.StopJob(player);
            Logger.Info("Saved Data for " + player.Name);
            player.GetCharacter()?.Logout();
            Players.Remove(player.GetAccountModel().Id);
            Accounts.Remove(player);
            Characters.Remove(player);
        }

        public void DoLogin(IPlayer player)
        {
            if (Accounts.ContainsKey(player))
            {
                Logger.Debug(player.Name + " already added!");
                return;
            }

            var account = player.GetAccountModel();
            player.SetData("account.id", account.Id);
            Players.Add(account.Id, player);
            Accounts.Add(player, account);
            Characters.Add(player, ContextFactory.Instance.CharacterModel.Local.FirstOrDefault(c => c.Id == account.CharacterId));
            player.GetCharacter().Login(player);
            player.Emit("afterLogin");
        }

        public bool DoesAccountExist(IPlayer player)
        {
            return ContextFactory.Instance.AccountModel.Local.Any(x => x.SocialClubName == player.SocialClubId.ToString());
        }

        public void PlayerReady(IPlayer player)
        {
            //player.SendInitialSync();
        }
    }
}