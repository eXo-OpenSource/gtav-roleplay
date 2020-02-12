using AltV.Net;
using server.Players;
using server.Shops;
using server.Shops.Types;

namespace server.Events

{
    internal class ShopEvents : IScript
    {
        [Event("Shop:VehicleInteraction")]
        public void OnVehicleShopInteraction(IPlayer player)
        {
            var shop = (VehicleShop) player.GetCharacter().GetInteractionData().SourceObject;
            shop.OpenVehicleShopMenu(player);
        }

        [Event("Shop:VehicleBuy")]
        public void OnVehicleShopBuy(IPlayer client, int shopId, int vehicleId)
        {
            var shop = Core.GetService<ShopManager>().Get<VehicleShop>(shopId);
            shop.BuyVehicle(client, vehicleId);
        }

        [Event("Shop:ItemInteraction")]
        public void OnItemShopInteraction(IPlayer player)
        {
            var shop = (ItemShop) player.GetCharacter().GetInteractionData().SourceObject;
            shop.OpenItemShopMenu(player);
        }

        [Event("Shop:ItemBuy")]
        public void OnItemShopBuy(IPlayer client, int shopId, int itemId)
        {
            var shop = Core.GetService<ShopManager>().Get<ItemShop>(shopId);
            shop.BuyItem(client, itemId);
        }
    }
}