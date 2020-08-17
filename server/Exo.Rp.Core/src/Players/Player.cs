using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.VisualBasic;
using Sentry.Protocol;
using server.Database;
using server.Environment;
using server.Players.Accounts;
using server.Players.Characters;
using server.Util.Log;
using server.World;

namespace server.Players
{
    public partial class Player : AltV.Net.Elements.Entities.Player, IPlayer
    {
        private static readonly Logger<Player> Logger = new Logger<Player>();

        private Account _account;
        private Account Account => _account ??= Core.GetService<DatabaseContext>().AccountModel.Local.FirstOrDefault(x => x.SocialClubId == SocialClubId);

        public User SentryContext => new User { Id = SocialClubId.ToString(), IpAddress = Ip, Username = Name, Other = new Dictionary<string, string> { { "HardwareIdHash", HardwareIdHash.ToString() }, { "AccountId", Account?.Id.ToString() }, { "EMail", Account?.EMail } }};

        public Player(IntPtr nativePointer, ushort id) : base(nativePointer, id)
        {
            Logger.Debug($"{ToString()} has joined the server.");
        }

        public new string ToString()
        {
#if DEBUG
            return $"({Name}, {Ip}, {HardwareIdHash})";
#else
            return $"({Name}, {HardwareIdHash})";
#endif
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
            return Account.Character;
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

        public void StopAnimation(bool force)
        {
            if (force)
            {
                Emit("Animation:ForceClear");
            }
            else
            {
                Emit("Animation:Clear");
            }
        }
        public void PlayAnimation(string animation, string v, int flag)
        {
            Emit("Animation:Start", v, animation, flag);
        }

        public void SendChatMessage(string msg)
        {
            Emit("Chat:Message", null, msg);
        }

        public void SetIntoVehicle(IVehicle veh, int seat)
        {
            Emit("Vehicle:SetIntoVehicle", veh.Id, seat);
        }

        public void RequestIpl(IEnumerable<string> ipls)
        {
            Core.GetService<IplManager>().RequestIpl(this, ipls);
        }

        public void RemoveIpl(IEnumerable<string> ipls)
        {
            Core.GetService<IplManager>().RemoveIpl(this, ipls);
        }

        public void RequestDefaulIpls()
        {
            Core.GetService<IplManager>().LoadDefaultIpls(this);
        }

        public void StartScenario(string name)
        {
            Emit("Scenario:Start", name);
        }

        public void ShowRadar(bool state)
        {
            Emit("HUD:ShowRadar", state);
        }

        public void HideHud(bool state = true)
        {
            Emit("HUD:Hide", state);
        }

        public void HideChat(bool state = true)
        {
            Emit("Chat:Hide", state);
        }

        public void HideUi(bool state = true)
        {
            HideHud(state);
            HideChat(state);
        }

        public void UpdateHud()
        {
            Emit("HUD:UpdateMoney", GetCharacter().GetMoney());
        }

    }
}