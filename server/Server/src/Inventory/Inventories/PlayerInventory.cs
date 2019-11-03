using server.Enums;
using server.Inventory.Items;
using server.Players;

namespace server.Inventory.Inventories
{
    public sealed class PlayerInventory : PermanentInventory
    {
        protected override void LoadDefaults()
        {
            AddBag(BagNames.Allgemein, 10, true, false);
            AddBag(BagNames.Weapon, 10, false, false);
            AddBag(BagNames.Nature, 10, false, false);

            AddItem(ItemManager.GetItemFromName("Smartphone"), 1, false);
        }

        protected override void AddBag(BagNames name, int slots = 5, bool selected = false, bool sync = true)
        {
            base.AddBag(name, slots, selected, sync);
            if (sync) SyncInventory(PlayerManager.GetClient(OwnerId));
        }

        public override void AddItem(Item item, int amount = 1, bool sync = true)
        {
            base.AddItem(item, amount, sync);
            if (sync) SyncInventory(PlayerManager.GetClient(OwnerId));
        }

        public override void RemoveItem(Item item, int amount = -1, bool sync = true)
        {
            base.RemoveItem(item, amount, sync);
            if (sync) SyncInventory(PlayerManager.GetClient(OwnerId));
        }
    }
}