using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using server.Players;

namespace server.Shops.Types
{
    internal class TuningShop : Shop
    {
        public TuningShop() : base()
        {
            // new Position(-1149.536, -1994.828, 12.76826)
        }

        protected override void OnPedColEnter(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;

            if (player.IsInVehicle) player.Vehicle.Position = new Position(-1149.536f, -1994.828f, 12.76826f);
        }
    }
}