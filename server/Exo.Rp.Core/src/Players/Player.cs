using System;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Extensions;
using server.Players.Accounts;
using server.Players.Characters;
using server.Util.Log;
using IPlayer = server.Players.IPlayer;

namespace server.Players
{
    public partial class Player : AltV.Net.Elements.Entities.Player, IPlayer
    {
        private static readonly Logger<Player> Logger = new Logger<Player>();
        
        private Account _account;
        public Account Account => _account ??= Core.GetService<DatabaseContext>().AccountModel?.Local.FirstOrDefault(x => x.SocialClubId == SocialClubId);
        private Character _character;
        public Character Character => _character ??= Core.GetService<DatabaseContext>().CharacterModel.Local.FirstOrDefault(c => c.Id == Account.CharacterId);

        public Player(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
            Logger.Debug($"{Name} has joined the server.");

            // Todo: only for debug, do not spawn the player here.
            Core.GetService<PlayerManager>().DoLogin(this);
        }

        public int GetId()
        {
            return Account.Id;
        }

        public Account GetAccount()
        {
            return Account;
        }

        public Character GetCharacter()
        {
            return Character;
        }
        public void SendNotification(string text)
        {
            Emit("sendNotification", text);
        }

        public void SendInformation(string text)
        {
            SendNotification("~b~Info~w~: " + text);
        }

        public void SendWarning(string text)
        {
            SendNotification("~o~Warnung~w~: " + text);
        }

        public void SendError(string text)
        {
            SendNotification("~r~Fehler~w~: " + text);
        }

        public void SendSuccess(string text)
        {
            SendNotification("~g~Erfolgreich~w~: " + text);
        }

        public void StopAnimation()
        {

        }
        public void PlayAnimation(string animation, string v, int flag)
        {

        }

        public void SendChatMessage(string msg)
        {

        }

        public void SetIntoVehicle(IVehicle veh, int seat)
        {

        }
    }
}