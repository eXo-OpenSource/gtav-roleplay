using Exo.Rp.Models.Enums;

namespace Exo.Rp.Core.Inventory.Inventories
{
    public sealed class VehicleInventory : PermanentInventory
    {
        public override void Load()
        {
            base.Load();
            LoadDefaults();
        }

        protected override void LoadDefaults()
        {
            base.LoadDefaults();
            AddBag(BagNames.Allgemein, 5, true, false);
            AddBag(BagNames.Weapon, 3, false, false);
        }
    }
}