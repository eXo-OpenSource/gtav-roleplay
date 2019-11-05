using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Enums;
using Newtonsoft.Json;

namespace server.Inventory
{
    [Table("Inventories")]
    public partial class Inventory
    {
        [Key]
        public int Id { get; set; }
        public InventoryType Type { get; set; }
        public OwnerType OwnerType { get; set; }
        public int OwnerId { get; set; }

        [NotMapped]
        public Dictionary<BagNames, BagModel> Bags { get; set; }

        [Column("Bags")]
        public string BagsSerialized
        {
            get => JsonConvert.SerializeObject(Bags);
            set => Bags = string.IsNullOrEmpty(value)
                ? new Dictionary<BagNames, BagModel>()
                : JsonConvert.DeserializeObject<Dictionary<BagNames, BagModel>>(value);
        }
    }
}