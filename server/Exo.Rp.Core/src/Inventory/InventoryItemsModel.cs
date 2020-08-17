using System.ComponentModel.DataAnnotations.Schema;
using server.Inventory.Items;

namespace server.Inventory
{
    [Table("InventoryItems")]
    public class InventoryItemsModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int InventoryId { get; set; }
        public int Amount { get; set; }
        public string Options { get; set; }

        [NotMapped]
        public Item Item
        {
            get => Core.GetService<ItemManager>().GetItem(ItemId);
            set => ItemId = value.Id;
        }

        [NotMapped]
        public PermanentInventory Inventory
        {
            get => Core.GetService<InventoryManager>().GetInventory<PermanentInventory>(InventoryId);
            set => InventoryId = value.Id;
        }
    }
}