using System.Collections.Generic;
using AltV.Net;
using server.Players;

namespace server.UI
{
    internal class DialogueManager : IScript
    {
        private static DialogueManager _instance;
        private readonly Dictionary<int, QuestionDialogue> _questionDialogues;

        public DialogueManager()
        {
            _questionDialogues = new Dictionary<int, QuestionDialogue>();
        }

        public static DialogueManager Instance => _instance ??= new DialogueManager();

        public int AddQuestionDialogue(QuestionDialogue dialogue)
        {
            var id = _questionDialogues.Count + 1;
            _questionDialogues.Add(id, dialogue);
            return id;
        }

        public void RemoveQuestionDialogue(int id)
        {
            _questionDialogues.Remove(id);
        }


        public void OnDialogueAccept(IPlayer player, int id)
        {
            if (_questionDialogues.ContainsKey(id))
                _questionDialogues[id].OnAccept(player);
            else
                player.SendError("Der Dialog ist abgebrochen!");
        }


        public void OnDialogueDecline(IPlayer player, int id)
        {
            if (_questionDialogues.ContainsKey(id))
                _questionDialogues[id].OnDecline(player);
            else
                player.SendError("Der Dialog ist abgebrochen!");
        }
    }
}