using System.Collections.Generic;
using System.Linq;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Extensions;
using server.Players.Accounts;
using server.Players.Characters;
using server.Util.Log;

namespace server.Players
{
    internal class PlayerManager
    {
        private static readonly Logger<PlayerManager> Logger = new Logger<PlayerManager>();

        private static readonly Dictionary<IPlayer, Account> Accounts;
        private static readonly Dictionary<IPlayer, Character> Characters;
        private static readonly Dictionary<int, IPlayer> Players;

        static PlayerManager()
        {
            Players = new Dictionary<int, IPlayer>();
            Characters = new Dictionary<IPlayer, Character>();
            Accounts = new Dictionary<IPlayer, Account>();
        }

        public static IPlayer GetClient(int accountId)
        {
            return Players.TryGetValue(accountId, out var client) ? client : null;
        }

        public static Character GetCharacter(IPlayer player)
        {
            if (player == null) return null;
            return Characters.TryGetValue(player, out var character) ? character : null;
        }

        public static Account GetAccount(IPlayer player)
        {
            if (player == null) return null;
            return Accounts.TryGetValue(player, out var account) ? account : null;
        }

        public static Account GetAccountBySerial(IPlayer player)
        {
            if (player == null) return null;
            foreach (var (_, accountModel) in Accounts)
            {
                if (accountModel.Serial == player.HardwareIdHash.ToString())
                    return accountModel;
            }

            return null;
        }

        public static string GetName(int accountId)
        {
            return ContextFactory.Instance.CharacterModel.Local.FirstOrDefault(x => x.Id == accountId)?.FullName ?? "Unbekannt";
        }

        public static bool IsPlayerOnline(int accountId)
        {
            return Players.ContainsKey(accountId);
        }

        public static void OnDisconnect(IPlayer player)
        {
            if (player.GetCharacter()?.IsJobActive() == true) player.GetCharacter()?.GetJob()?.StopJob(player);
            Logger.Info("Saved Data for " + player.Name);
            player.GetCharacter()?.Logout();
            Players.Remove(player.GetAccountModel().Id);
            Accounts.Remove(player);
            Characters.Remove(player);
        }

        public static void DoLogin(IPlayer player)
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

        public static bool DoesAccountExist(IPlayer player)
        {
            return ContextFactory.Instance.AccountModel.Local.Any(x => x.SocialClubName == player.SocialClubId.ToString());
        }

        public static void PlayerReady(IPlayer player)
        {
            //player.SendInitialSync();
        }
    }
}