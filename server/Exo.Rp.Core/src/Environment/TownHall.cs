using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Exo.Rp.Core.Players.Characters;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Environment
{
    internal class TownHall
    {
        static TownHall()
        {
            Alt.Log("test");

            var pos = new Position(236.3676f, -409.105f, 47.92431f);

            var blip = Alt.CreateBlip(BlipType.Area, pos);
            var col = Alt.CreateColShapeSphere(pos, 3);
            //col.OnEntityEnterColShape += OnColEnter;
            //col.OnEntityExitColShape += OnColExit;
        }

        private static void OnColEnter(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;
            var interactionData = new InteractionData
            {
                SourceObject = new TownHall(), // TODO: ??????
                CallBack = null
            };
            player.GetCharacter()
                .ShowInteraction("Stadthalle", "onTownHallInteraction", interactionData: interactionData);
        }

        private static void OnColExit(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;
            player.GetCharacter().HideInteraction();
        }

        public static void OnInteract(IPlayer player)
        {
            player.Emit("showTownHallMenu");
        }
    }
}