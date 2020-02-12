using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Players.Accounts;
using server.Players.Characters;

namespace server.Players
{
    public interface IPlayer : AltV.Net.Elements.Entities.IPlayer
    {
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
        void StopAnimation();
        void PlayAnimation(string animation, string v, int flag);
        void SendChatMessage(string msg);
        void SetIntoVehicle(IVehicle veh, int seat);
    }
}