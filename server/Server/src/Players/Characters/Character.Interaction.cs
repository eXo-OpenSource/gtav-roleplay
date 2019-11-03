using System;
using Newtonsoft.Json;
//using shared.Interactions;

namespace server.Players.Characters
{
    public partial class Character
    {

        public int ShowInteraction(string title, string eventName, string text = "Drücke E um zu Interagieren",
            string key = "E", InteractionData interactionData = null)
        {
            var id = new Random().Next(1, 999999);
            /*var newInteraction = new InteractionDto
            {
                Title = title,
                ServerId = id,
                Id = id.ToString(),
                ServerEvent = eventName,
                Text = text,
                Key = key
            };
            if (interactionData != null) InteractionData.Add(newInteraction.ServerId, interactionData);
            LastInteractionId = newInteraction.ServerId;
            _player.Emit("Interaction:Show", JsonConvert.SerializeObject(newInteraction));
            return newInteraction.ServerId;
            */
            return 0;
        }

        public void HideInteraction(int id = 0)
        {
            if (id == 0) id = LastInteractionId;
            _player.Emit("Interaction:Hide", id);
            InteractionData.Remove(id);
        }

        public InteractionData GetInteractionData(int id = 0)
        {
            if (id == 0) id = LastInteractionId;
            InteractionData.TryGetValue(id, out var data);
            return data;
        }
        
    }

    public class InteractionData
    {
        public object SourceObject { get; set; }
        public object CallBack { get; set; }
    }
}