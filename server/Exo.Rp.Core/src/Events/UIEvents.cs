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
