using System.Collections.Generic;

namespace models.Enums
{
    // TODO: Need to be move out of enums namespace
    public static class InventoryBagData
    {
        public static Dictionary<BagNames, Bag> Bags = new Dictionary<BagNames, Bag>
        {
            {
                BagNames.Allgemein, new Bag
                {
                    Id = 0,
                    Name = "Allgemein",
                    Icon = "backpack.png"
                }
            },
            {
                BagNames.Weapon, new Bag
                {
                    Id = 1,
                    Name = "Waffen",
                    Icon = "weapon.png"
                }
            },
            {
                BagNames.Nature, new Bag
                {
                    Id = 2,
                    Name = "Natur",
                    Icon = "nature.png"
                }
            }
        };

        public class Bag
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Icon { get; set; }
        }
    }
}