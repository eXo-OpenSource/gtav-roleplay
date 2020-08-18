using Exo.Rp.Core.Inventory.Items;
using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Inventory.Inventories
{
    public sealed class TeamInventory : PermanentInventory
    {
        public override void Load()
        {
            base.Load();
            LoadDefaults();
        }

        protected override void LoadDefaults()
        {
            base.LoadDefaults();
            AddBag(BagNames.Allgemein, 10, true, false);
            AddBag(BagNames.Weapon, 10, false, false);

            AddItem(Core.GetService<ItemManager>().GetItem("Smartphone"), 1, false);
        }
    }
}