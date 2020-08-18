using AltV.Net;
using server.Players;
using server.UI;

namespace server.Events

{
    internal class UiEvents : IScript
    {
        [ClientEvent("Ui:ShowUi")]
        public void ShowUi(IPlayer player, bool state = true)
        {
            player.HideUi(state);
        }

        [ClientEvent("onQuestionDialogueAccept")]
        public void OnDialogueAccept(IPlayer player, int id)
        {
            DialogueManager.Instance.OnDialogueAccept(player, id);
        }

        [ClientEvent("onQuestionDialogueDecline")]
        public void OnDialogueDecline(IPlayer player, int id)
        {
            DialogueManager.Instance.OnDialogueDecline(player, id);
        }
    }
}