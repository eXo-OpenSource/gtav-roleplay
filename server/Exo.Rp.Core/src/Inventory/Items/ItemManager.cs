using System;
using System.Collections.Generic;
using System.Linq;
using models.Enums;
using server.Database;
using server.Util.Log;

namespace server.Inventory.Items
{
    internal class ItemManager : IManager
    {
        private static readonly Logger<ItemManager> Logger = new Logger<ItemManager>();

        private readonly Dictionary<int, Item> Items;

        public ItemManager()
        {
            Items = new Dictionary<int, Item>();

            if (!ContextFactory.Instance.ItemModel.Local.Any())
            {
                Logger.Warn("WARNING no Items in the database found.");
                ContextFactory.Instance.ItemModel.Local.Add(new Item
                {
                    Name = "Smartphone",
                    SubText = "Stk.",
                    Icon = "smartphone.png",
                    Bag = BagNames.Allgemein,
                    Stackable = true
                });
                ContextFactory.Instance.ItemModel.Local.Add(new Item
                {
                    Name = "Apfel",
                    SubText = "Stk.",
                    Icon = "apple.png",
                    Bag = BagNames.Nature,
                    Stackable = true
                });
            }

            foreach (var item in ContextFactory.Instance.ItemModel.Local)
            {
                Items.Add(item.Id, item);
            }
        }

        public Item GetItem(int itemId)
        {
            return Items.TryGetValue(itemId, out var item) ? item : null;
        }

        public Item GetItem(ItemModel itemModel)
        {
            return Items.FirstOrDefault(x => x.Value.ItemModel == itemModel).Value;
        }

        [Obsolete("Use GetItem(ItemModel) instead.")]
        public Item GetItem(string itemName)
        {
            return Items.FirstOrDefault(x => x.Value.Name.Equals(itemName)).Value;
        }
    }
}