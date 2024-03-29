using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using IPlayer = Exo.Rp.Core.Players.IPlayer;

namespace Exo.Rp.Core.Shops.Types
{
    internal class TuningShop : Shop
    {
        public TuningShop() : base()
        {
            // new Position(-1149.536, -1994.828, 12.76826)
        }

        protected override void OnPedColEnter(Colshape.Colshape colshape, IEntity entity)
        {
            if(!(entity is IPlayer player)) return;
            if (player.GetCharacter() == null) return;

            if (player.IsInVehicle) player.Vehicle.Position = new Position(-1149.536f, -1994.828f, 12.76826f);
        }
    }
}