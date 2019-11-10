using System;
using System.Linq;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Database;
using server.Players.Accounts;
using server.Players.Characters;
using server.Util.Log;

namespace server.Players
{
    public partial class Player : AltV.Net.Elements.Entities.Player, IPlayer
    {
        private static readonly Logger<Player> Logger = new Logger<Player>();
        
        private Account _account;
        private Account Account => _account ??= Core.GetService<DatabaseContext>().AccountModel?.Local.FirstOrDefault(x => x.SocialClubId == SocialClubId);
        private Character _character;
        private Character Character => _character ??= Core.GetService<DatabaseContext>().CharacterModel.Local.FirstOrDefault(c => c.Id == Account.CharacterId);

        public Player(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
            Logger.Debug($"({Name}, {Ip}, {HardwareIdHash:x8}) has joined the server.");
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

        public new void Spawn(Position position, uint delayMs = 0U)
        {
            base.Spawn(position, delayMs);
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