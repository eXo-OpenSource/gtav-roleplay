using System.Collections.Generic;
using AutoMapper;

namespace server.Inventory
{
    internal class InventoryManager
    {
        public static Dictionary<int, Inventory> Inventories;

        static InventoryManager()
        {
            Inventories = new Dictionary<int, Inventory>();
        }

        public static void AddInventory<T>(Inventory inventory) where T : Inventory
        {
            //Inventories.Add(inventory.Id, Mapper.Map<T>(inventory));
        }

        public static T GetInventory<T>(int id) where T : Inventory
        {
            if (Inventories.ContainsKey(id))
            {
                return (T) Inventories[id];
            }

            return null;
        }
    }
}