using System.Linq;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Players.Accounts;
using Character = server.Players.Characters.Character;
using IPlayer = server.Players.Interfaces.IPlayer;

namespace server.Players
{
    public static class PlayerExtensions
    {
        public static Account GetAccountModel(this Interfaces.IPlayer player)
        {
            return ContextFactory.Instance.AccountModel?.Local.FirstOrDefault(x => x.SocialClubName == player.SocialClubId.ToString());
        }
    }
}
