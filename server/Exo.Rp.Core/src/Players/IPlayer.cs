using System.Collections.Generic;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Accounts;
using Exo.Rp.Core.Players.Characters;
using Sentry.Protocol;

namespace Exo.Rp.Core.Players
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
        void SendCorrelationId(SentryId id);
        void SendSuccess(string text);
        void StopAnimation(bool force = false);
        void PlayAnimation(string animation, string v, int flag);
        void SendChatMessage(string sendername, string msg);
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