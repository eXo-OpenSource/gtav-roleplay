using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AltV.Net.Elements.Entities;
using models.Enums;
using Newtonsoft.Json;
using server.Inventory.Items;
using server.Players.Characters;
using server.Util.Log;
using IPlayer = server.Players.IPlayer;

namespace server.Shops.Types
{
    internal class ItemShop : Shop
    {
        [NotMapped]
        private static readonly Logger<ItemShop> Logger = new Logger<ItemShop>();
        [NotMapped]
        private Dictionary<int, ShopItemData> _itemData;
        [NotMapped]
        private ItemShopOptions _options;

        public ItemShop() : base()
        {
            LoadItems();
        }

        protected override void OnPedColEnter(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;
            var interactionData = new InteractionData
            {
                SourceObject = this,
                CallBack = null
            };
            player.GetCharacter()
                .ShowInteraction(PedModel.Name, "Shop:ItemInteraction", interactionData: interactionData);
        }

        protected override void OnPedColExit(ColShape colshape, IPlayer player)
        {
            if (player.GetCharacter() == null) return;
            player.GetCharacter().HideInteraction();
        }

        public void OpenItemShopMenu(IPlayer player)
        {
            var data = new BuyItemMenuData
            {
                Id = Id,
                Name = Name,
                Items = _itemData
            };

            player.Emit("outputIPlayerConsole", JsonConvert.SerializeObject(data), false);
            player.Emit("Shop:ItemShowMenu", JsonConvert.SerializeObject(data));
        }


        private void LoadItems()
        {
            _itemData = new Dictionary<int, ShopItemData>();
            _options = ItemShopOptions;

            if (_options?.Items != null)
                foreach (var item in _options.Items)
                {
                    var nItemData = Core.GetService<ItemManager>().GetItem(item.Key);
                    var nData = new ShopItemData
                    {
                        Id = item.Key,
                        Price = item.Value,
                        Name = nItemData.Name
                    };

                    _itemData.Add(nData.Id, nData);
                }
            else
                Logger.Info("No Items for ShopId " + Id + " found!");
        }

        public override void BuyItem(IPlayer player, int itemId)
        {
            if (_itemData.TryGetValue(itemId, out var selected))
                if (player.GetCharacter().GetMoney() >= selected.Price)
                    if (player.GetCharacter().TransferMoneyToShop(this, selected.Price, "Item-Kauf",
                        MoneyTransferCategory.Item, MoneyTransferSubCategory.Buy))
                    {
                        player.GetCharacter().Inventory.AddItem(Core.GetService<ItemManager>().GetItem(itemId), 1);
                        return;
                    }

            player.SendError("Das Item konnte nicht gekauft werden!");
        }

        private class ShopItemData
        {
            public string Name;
            public int Price;
            public int Id { get; set; }
        }

        [Serializable]
        private class BuyItemMenuData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Dictionary<int, ShopItemData> Items { get; set; }
        }
    }
}