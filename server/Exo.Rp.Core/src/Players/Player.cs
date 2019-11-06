using System;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Peds;
using server.Players.Accounts;
using server.Players.Characters;
using server.Util.Log;

namespace server.Players
{
    public partial class Player : AltV.Net.Elements.Entities.Player, Interfaces.IPlayer
    {
        private static readonly Logger<Player> Logger = new Logger<Player>();

        private Account Account => ContextFactory.Instance.AccountModel?.Local.FirstOrDefault(x => x.SocialClubName == SocialClubId.ToString());

        private Character Character => ContextFactory.Instance.CharacterModel.Local.FirstOrDefault(c => c.Id == Account.CharacterId);

        public Player(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
            Logger.Debug($"{Name} has joined the server.");

            // Todo: only for debug, do not spawn the player here.
            Model = 1885233650;
            Spawn(new Position(0, 0, 70), 0);
        }

        public int GetId()
        {
            return IsLoggedIn() ? Account.Id : default;
        }

        public bool IsLoggedIn()
        {
            return true;
        }

        public Account GetAccount()
        {
            return IsLoggedIn() ? Account : default;
        }

        public Character GetCharacter()
        {
            return IsLoggedIn() ? Character : default;
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