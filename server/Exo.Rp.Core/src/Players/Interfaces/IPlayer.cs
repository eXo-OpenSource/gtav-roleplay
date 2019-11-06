using AltV.Net.Elements.Entities;
using server.Players.Accounts;
using server.Players.Characters;

namespace server.Players.Interfaces
{
    public interface IPlayer : AltV.Net.Elements.Entities.IPlayer
    {
        int GetId();
        bool IsLoggedIn();
        Account GetAccount();
        Character GetCharacter();
        void SendNotification(string text);
        void SendInformation(string text);
        void SendWarning(string text);
        void SendError(string text);
        void SendSuccess(string text);
        void StopAnimation();
        void PlayAnimation(string animation, string v, int flag);
        void SendChatMessage(string msg);
        void SetIntoVehicle(IVehicle veh, int seat);
    }
}