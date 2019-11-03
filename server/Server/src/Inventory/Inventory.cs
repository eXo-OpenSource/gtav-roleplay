using System.Collections.Generic;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;
using server.Enums;
using server.Inventory.Items;
using server.Util.Log;
using shared.Inventory;

namespace server.Inventory
{
    public partial class Inventory
    {
       
        public virtual void Load()
        {

        }
        protected virtual void LoadDefaults()
        {

        }

        protected virtual void AddBag(BagNames name, int slots = 5, bool selected = false, bool sync = true)
        {
            if (!InventoryBagData.Bags.TryGetValue(name, out var bagInfos))
            {
                //Logger.Debug("Invalid Bagdata!");
                return;
            }

            if (BagMap.TryGetValue(name, out var bag)) return;

            var nBag = new BagDto
            {
                Id = bagInfos.Id,
                Name = bagInfos.Name,
                Icon = bagInfos.Icon,
                Selected = selected,
                Items = new Dictionary<int, ItemDto>()
            };

            BagMap.Add(name, nBag);
        }

        public virtual void AddItem(Item item, int amount = 1, bool sync = true)
        {
            if (!BagMap.TryGetValue(item.Bag, out var bag))
            {
                Logger.Debug("AddItem Error: Bag not available!");
                return;
            }

            if (bag.Items.TryGetValue(item.Id, out var itemData))
                itemData.Amount = itemData.Amount + amount;
            else
                bag.Items.Add(item.Id, new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = amount,
                    Icon = item.Icon,
                    Subtext = item.SubText
                });
        }

        public virtual void RemoveItem(Item item, int amount = -1, bool sync = true)
        {
            if (!BagMap.TryGetValue(item.Bag, out var bag))
            {
                Logger.Debug("AddItem Error: Bag not available!");
                return;
            }

            if (bag.Items.TryGetValue(item.Id, out var itemData))
            {
                if (amount == -1 || amount > itemData.Amount)
                    bag.Items.Remove(item.Id);
                else
                    itemData.Amount = itemData.Amount - amount;
            }
        }

        public int GetItemCount(Item item)
        {
            if (!BagMap.TryGetValue(item.Bag, out var bag))
            {
                Logger.Debug("GetItemCount Error: Bag not available!");
                return 0;
            }

            if (bag.Items.TryGetValue(item.Id, out var itemData)) return itemData.Amount;
            return 0;
        }


        public void SyncInventory(IPlayer player)
        {
            var nBags = new List<BagDto>();
            foreach (var bagData in BagMap.Values) nBags.Add(bagData);

            var nData = new InventoryDto
            {
                Id = Id,
                Title = Title,
                Size = Size,
                Bags = nBags
            };
            //Logger.Debug("SYNC INVENTORY:");
            //Logger.Debug(JsonConvert.SerializeObject(nData));
            //player.Emit("outputIPlayerConsole", Newtonsoft.Json.JsonConvert.SerializeObject(nData), false);
            player.Emit("syncInventory", Id, JsonConvert.SerializeObject(nData));
        }
    }
}