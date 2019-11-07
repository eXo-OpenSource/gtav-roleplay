using System.Linq;
using Microsoft.EntityFrameworkCore;
using models.Enums;
using server.Database;
using server.Inventory.Items;

namespace server.Inventory
{
    public partial class PermanentInventory : Inventory
    {
        public override void Load()
        {
            LoadDatabaseBags();
            LoadDatabaseItems();
        }

        protected override void LoadDefaults()
        {
            
        }

        protected override void AddBag(BagNames name, int slots = 5, bool selected = false, bool sync = true)
        {
            base.AddBag(name, slots);

            if (Bags.ContainsKey(name)) return;
            var nModel = new BagModel
            {
                Type = name,
                Slots = slots
            };

            Bags.Add(name, nModel);
            Save();
        }

        public override void AddItem(Item item, int amount = 1, bool sync = true)
        {
            base.AddItem(item, amount);

            if (!BagMap.TryGetValue(item.Bag, out var bag))
            {
                PermanentInventory.Logger.Debug("AddItem Error: Bag not available!");
                return;
            }

            if (bag.Items.ContainsKey(item.Id))
            {
                var items = Core.GetService<DatabaseContext>().InventoryItemsModel.Local
                    .Where(x => x.ItemId == item.Id && x.Inventory == this);

                if (items.Any())
                {
                    PermanentInventory.Logger.Debug("AddItem: Increased Amount!");
                    items.First().Amount = items.First().Amount + amount;
                    Core.GetService<DatabaseContext>().Entry(items.First()).State = EntityState.Modified;
                }
            }
            else
            {
                var nItem = new InventoryItemsModel
                {
                    Inventory = this,
                    Amount = amount,
                    ItemId = item.Id
                };
                Core.GetService<DatabaseContext>().InventoryItemsModel.Add(nItem);
            }
        }

        public void Save()
        {
            Core.GetService<DatabaseContext>().Entry(this).State = EntityState.Modified;
        }

        public void LoadDatabaseBags()
        {
            foreach (var entry in Bags) base.AddBag(entry.Key, entry.Value.Slots);
        }

        public void LoadDatabaseItems()
        {
            var items = Core.GetService<DatabaseContext>().InventoryItemsModel.Local.Where(x => x.Inventory == this);
            foreach (var model in items) base.AddItem(Core.GetService<ItemManager>().GetItem(model.Id), model.Amount);
        }
    }
}