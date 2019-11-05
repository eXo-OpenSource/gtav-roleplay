using System.ComponentModel.DataAnnotations.Schema;
using models.Enums;

namespace server.Inventory.Items
{
    [Table("Items")]
    public partial class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubText { get; set; }
        public string Icon { get; set; }
        public BagNames Bag { get; set; }

        [Column(TypeName = "tinyint(1)")]
        public bool Stackable { get; set; }
    }
}