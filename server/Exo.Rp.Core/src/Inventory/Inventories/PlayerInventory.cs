using Exo.Rp.Core.Inventory.Items;
using Exo.Rp.Core.Players;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Inventory.Inventories
{
    public sealed class PlayerInventory : PermanentInventory
    {
        protected override void LoadDefaults()
        {
            AddBag(BagNames.Allgemein, 10, true, false);
            AddBag(BagNames.Weapon, 10, false, false);
            AddBag(BagNames.Nature, 10, false, false);

            AddItem(Core.GetService<ItemManager>().GetItem(ItemModel.Smartphone), 1, false);
        }

        protected override void AddBag(BagNames name, int slots = 5, bool selected = false, bool sync = true)
        {
            base.AddBag(name, slots, selected, sync);
            if (sync) SyncInventory(Core.GetService<PlayerManager>().GetClient(OwnerId));
        }

        public override void AddItem(Item item, int amount = 1, bool sync = true)
        {
            base.AddItem(item, amount, sync);
            if (sync) SyncInventory(Core.GetService<PlayerManager>().GetClient(OwnerId));
        }

        public override void RemoveItem(Item item, int amount = -1, bool sync = true)
        {
            base.RemoveItem(item, amount, sync);
            if (sync) SyncInventory(Core.GetService<PlayerManager>().GetClient(OwnerId));
        }
    }
}