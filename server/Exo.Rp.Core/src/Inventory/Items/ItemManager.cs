using System;
using System.Collections.Generic;
using System.Linq;
using Exo.Rp.Core.Database;
using Exo.Rp.Core.Util.Log;
using Exo.Rp.Models.Enums;
using Exo.Rp.Sdk;
using Exo.Rp.Sdk.Logger;

namespace Exo.Rp.Core.Inventory.Items
{
    internal class ItemManager : IManager
    {
        private static readonly ILogger<ItemManager> Logger = new Logger<ItemManager>();

        private readonly DatabaseContext _databaseContext;
        private readonly Dictionary<int, Item> _items;

        public ItemManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _items = new Dictionary<int, Item>();

            if (!_databaseContext.ItemModel.Local.Any())
            {
                Logger.Warn("WARNING no Items in the database found.");
                _databaseContext.ItemModel.Local.Add(new Item
                {
                    Id = 1,
                    Name = "Smartphone",
                    SubText = "Stk.",
                    Icon = "smartphone.png",
                    Bag = BagNames.Allgemein,
                    Stackable = true
                });
                _databaseContext.ItemModel.Local.Add(new Item
                {
                    Id = 2,
                    Name = "Apfel",
                    SubText = "Stk.",
                    Icon = "apple.png",
                    Bag = BagNames.Nature,
                    Stackable = true
                });
            }

            foreach (var item in _databaseContext.ItemModel.Local)
            {
                _items.Add(item.Id, item);
            }
        }

        public Item GetItem(int itemId)
        {
            return _items.TryGetValue(itemId, out var item) ? item : null;
        }

        public Item GetItem(ItemModel itemModel)
        {
            return _items.FirstOrDefault(x => x.Value.ItemModel == itemModel).Value;
        }

        [Obsolete("Use GetItem(ItemModel) instead.")]
        public Item GetItem(string itemName)
        {
            return _items.FirstOrDefault(x => x.Value.Name.Equals(itemName)).Value;
        }
    }
}