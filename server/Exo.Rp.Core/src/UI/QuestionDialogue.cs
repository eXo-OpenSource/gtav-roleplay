using System;
using server.Players;

namespace server.UI
{
    internal class QuestionDialogue : Dialogue
    {
        private readonly Action<IPlayer, IPlayer> _accept;
        private readonly Action<IPlayer, IPlayer> _decline;
        private readonly int _id;
        private readonly IPlayer _source;
        private readonly IPlayer _target;
        private readonly string _text;
        private readonly string _title;

        private QuestionDialogue(IPlayer source, IPlayer target, string title, string text, Action<IPlayer, IPlayer> accept,
            Action<IPlayer, IPlayer> decline)
        {
            _id = DialogueManager.Instance.AddQuestionDialogue(this);
            _target = target;
            _source = source;
            _title = title;
            _text = text;
            _accept = accept;
            _decline = decline;
        }

        public static void Create(IPlayer source, IPlayer target, string title, string text,
            Action<IPlayer, IPlayer> accept,
            Action<IPlayer, IPlayer> decline)
        {
            var dialogue = new QuestionDialogue(source, target, title, text, accept, decline);
            target.Emit("openQuestionDialogue", title, text, dialogue._id);
        }

        public void OnAccept(IPlayer player)
        {
            _accept(_source, _target);
            DialogueManager.Instance.RemoveQuestionDialogue(_id);
        }

        public void OnDecline(IPlayer player)
        {
            _decline(_source, _target);
            DialogueManager.Instance.RemoveQuestionDialogue(_id);
        }
    }
}