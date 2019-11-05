using System.Collections.Generic;
using AutoMapper;
using server.AutoMapper;

namespace server.Inventory
{
    internal class InventoryManager
    {
        public static readonly Dictionary<int, Inventory> Inventories;

        static InventoryManager()
        {
            Inventories = new Dictionary<int, Inventory>();
        }

        public static void AddInventory<T>(Inventory inventory) 
            where T : Inventory
        {
            Inventories.Add(inventory.Id, AutoMapperConfiguration.GetMapper().Map<T>(inventory));
        }

        public static T GetInventory<T>(int id) 
            where T : Inventory
        {
            if (Inventories.ContainsKey(id))
            {
                return Inventories[id] as T;
            }

            return default;
        }
    }
}