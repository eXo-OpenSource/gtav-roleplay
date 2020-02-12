using System.Collections.Generic;
using server.AutoMapper;

namespace server.Inventory
{
    internal class InventoryManager : IManager
    {
        public readonly Dictionary<int, Inventory> Inventories;

        public InventoryManager()
        {
            Inventories = new Dictionary<int, Inventory>();
        }

        public void AddInventory<T>(Inventory inventory) 
            where T : Inventory
        {
            Inventories.Add(inventory.Id, AutoMapperConfiguration.GetMapper().Map<T>(inventory));
        }

        public T GetInventory<T>(int id) 
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