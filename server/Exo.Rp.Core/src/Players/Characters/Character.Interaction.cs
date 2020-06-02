using System;

//using shared.Interactions;

namespace server.Players.Characters
{
    public partial class Character
    {

        public string ShowInteraction(string title, string eventName, string text = "Drücke E um zu Interagieren",
            int key = 69, string ident = null, InteractionData interactionData = null)
        {
            ident ??= new Random().Next(1, 999999).ToString();
            if (interactionData != null) InteractionData.Add(ident, interactionData);
            LastInteractionId = ident;

            _player.Emit("Interaction:Show", ident, title, text, eventName);
            return ident;
        }

        public void HideInteraction(string id = null)
        {
            if (id == null) id = LastInteractionId;
            _player.Emit("Interaction:Hide", id);
            InteractionData.Remove(id);
        }

        public InteractionData GetInteractionData(string id = null)
        {
            if (id == null) id = LastInteractionId;
            InteractionData.TryGetValue(id, out var data);
            return data;
        }

    }
}
