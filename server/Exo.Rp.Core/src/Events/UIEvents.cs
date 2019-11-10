using AltV.Net;
using server.Players;
using server.UI;

namespace server.Events

{
    internal class UiEvents : IScript
    {
        [Event("onQuestionDialogueAccept")]
        public void OnDialogueAccept(IPlayer player, int id)
        {
            DialogueManager.Instance.OnDialogueAccept(player, id);
        }

        [Event("onQuestionDialogueDecline")]
        public void OnDialogueDecline(IPlayer player, int id)
        {
            DialogueManager.Instance.OnDialogueDecline(player, id);
        }
    }
}