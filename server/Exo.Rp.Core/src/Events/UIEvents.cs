using AltV.Net;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.UI;

namespace Exo.Rp.Core.Events

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