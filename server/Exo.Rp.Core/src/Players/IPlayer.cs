using System.Collections.Generic;
using System.Threading.Tasks;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Sentry.Protocol;
using server.Players.Accounts;
using server.Players.Characters;

namespace server.Players
{
    public interface IPlayer : AltV.Net.Elements.Entities.IPlayer
    {
        public User SentryContext { get; }

        string ToString();

        int GetId();
        Account GetAccount();
        Character GetCharacter();
        new void Spawn(Position position, uint delayMs = 0U);
        void SendNotification(string text);
        void SendInformation(string text);
        void SendWarning(string text);
        void SendError(string text);
        void SendSuccess(string text);
        void StopAnimation(bool force = false);
        void PlayAnimation(string animation, string v, int flag);
        void SendChatMessage(string msg);
        void SetIntoVehicle(IVehicle veh, int seat);
        void StartScenario(string name);
        void RequestIpl(IEnumerable<string> ipls);
        void RemoveIpl(IEnumerable<string> ipls);
        void RequestDefaulIpls();
        void UpdateHud();
        void ShowRadar(bool state);
        void HideChat(bool state);
        void HideHud(bool state);
        void HideUi(bool state);
    }
}