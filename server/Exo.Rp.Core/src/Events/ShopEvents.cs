using AltV.Net;
using Exo.Rp.Core.Players;
using Exo.Rp.Core.Shops;
using Exo.Rp.Core.Shops.Types;

namespace Exo.Rp.Core.Events

{
    internal class ShopEvents : IScript
    {
        [ClientEvent("Shop:VehicleInteraction")]
        public void OnVehicleShopInteraction(IPlayer player)
        {
            var shop = (VehicleShop) player.GetCharacter().GetInteractionData().SourceObject;
            shop.OpenVehicleShopMenu(player);
        }

        [ClientEvent("Shop:VehicleBuy")]
        public void OnVehicleShopBuy(IPlayer client, int shopId, int vehicleId)
        {
            var shop = Core.GetService<ShopManager>().Get<VehicleShop>(shopId);
            shop.BuyVehicle(client, vehicleId);
        }

        [ClientEvent("Shop:ItemInteraction")]
        public void OnItemShopInteraction(IPlayer player)
        {
            var shop = (ItemShop) player.GetCharacter().GetInteractionData().SourceObject;
            shop.OpenItemShopMenu(player);
        }

        [ClientEvent("Shop:ItemBuy")]
        public void OnItemShopBuy(IPlayer client, int shopId, int itemId)
        {
            var shop = Core.GetService<ShopManager>().Get<ItemShop>(shopId);
            shop.BuyItem(client, itemId);
        }
    }
}