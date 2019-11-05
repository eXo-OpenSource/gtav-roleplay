using System.Collections.Generic;
using System.Linq;
using server.Database;
using server.Enums;
using server.Util.Log;

namespace server.Inventory.Items
{
    internal class ItemManager
    {
        private static readonly Logger<ItemManager> Logger = new Logger<ItemManager>();

        private static readonly Dictionary<int, Item> Items;
        private static readonly Dictionary<string, Item> ItemsMap;

        static ItemManager()
        {
            Items = new Dictionary<int, Item>();
            ItemsMap = new Dictionary<string, Item>();

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
                ItemsMap.Add(item.Name, item);
            }
        }

        public static Item GetItem(int itemId)
        {
            return Items.TryGetValue(itemId, out var item) ? item : null;
        }

        public static Item GetItemFromName(string itemName)
        {
            return ItemsMap.TryGetValue(itemName, out var item) ? item : null;
        }
    }
}